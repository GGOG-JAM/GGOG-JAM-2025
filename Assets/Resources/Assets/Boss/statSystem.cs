using System.Collections;
using UnityEngine;

public class statSystem : MonoBehaviour
{
    Rigidbody2D rb;
    bossSkills bs;

    public float currentHp, maxHp;
    private float prevHp;
    public int handler = 0; 

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
        bs = GetComponent<bossSkills>();
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
            switch (currentHp, handler)
            {
                case (90,0):
                    bs.CoinFlip(); CameraShake.Instance.Shake(0.3f, 1f); handler++; break;
                case (80,1):
                    bs.SpinSlot(); handler++; break;
                case (70,2):
                    bs.PlayMineFarm(); handler++; break;
                case (60,3):
                    bs.CoinFlip(); handler++; break;
                case (50,4):
                    bs.SpinSlot(); handler++; break;
                case (40,5):
                    bs.PlayMineFarm(); handler++; break;
                case (30,6):
                    bs.CoinFlip(); handler++; break;
                case (20,7):
                    bs.CoinFlip(); handler++; break;
                case (10,8):
                    bs.CoinFlip(); handler++; break;

            }
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
