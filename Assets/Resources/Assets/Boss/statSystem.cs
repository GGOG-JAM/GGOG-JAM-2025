using System.Collections;
using UnityEngine;

public class statSystem : MonoBehaviour
{
    Rigidbody2D rb;

    public float currentHp, maxHp;
    private float prevHp;

    public bool isPlayerAlive;
    public bool canGetDamage;

    private float timeSinceLastHit = 0f;

    void Start()
    {
        currentHp = maxHp;
        prevHp = currentHp;
        isPlayerAlive = true;
        canGetDamage = true;

        rb = GetComponent<Rigidbody2D>();
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
}
