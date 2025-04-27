using System.Collections;
using UnityEngine;

public class statSystemForPlayer : MonoBehaviour
{
    Rigidbody2D rb;
    bossSkills bs;

    public float currentHp, maxHp;
    private float prevHp;
    public int handler = 0;

    public bool isPlayerAlive;
    public bool canGetDamage;

    private float timeSinceLastHit = 0f;

    private SpriteRenderer spriteRenderer;
    Material material;

    void Start()
    {
        currentHp = maxHp;
        prevHp = currentHp;
        isPlayerAlive = true;
        canGetDamage = true;

        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;

        rb = GetComponent<Rigidbody2D>();
        bs = GetComponent<bossSkills>();
        Flash();
    }

    private void Update()
    {
        isHpChange();
    }

    void isHpChange()
    {

        if (prevHp != currentHp)
        {
            isPlayerDead();
        }
    }

    public void isPlayerDead()
    {
        if (currentHp <= 0)
        {
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        isPlayerAlive = false;
        Debug.Log("Player died!");
        this.enabled = false;
    }

    public void GetDamage(float hasarMiktari)
    {
        if (canGetDamage)
        {
            canGetDamage = false;
            prevHp = currentHp;
            currentHp -= hasarMiktari;
            currentHp = Mathf.Clamp(currentHp, 0, maxHp);
            Flash();
            isPlayerDead();

            timeSinceLastHit = Time.time - timeSinceLastHit;
            StartCoroutine(DamageCD());
        }
    }


    private IEnumerator DamageCD()
    {
        yield return new WaitForSeconds(0.5f);
        canGetDamage = true;
    }


    private void Flash()
    {
        StartCoroutine(FlashCoroutine());
    }

    private System.Collections.IEnumerator FlashCoroutine()
    {
        material.SetFloat("_FlashAmount", 1); // Özel shader kullanýyorsan
        yield return new WaitForSeconds(0.1f);
        material.SetFloat("_FlashAmount", 0);
    }
}
