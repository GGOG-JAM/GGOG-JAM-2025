using System.Collections;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Flux : MonoBehaviour
{

    public int _damage = 10;
    public float _cooldown = 1f;
    public bool _canCast = true;
    public int _magicCount = 1;

    private float elapsedTime = 0f;
    public float duration = 1.0f;


    public float _damageRange = 2f;
    public LayerMask _damageLayer = 0;

    public GameObject _magicPrefab;

    private void Update()
    {
        if (_canCast)
        {
            CastSpell();
        }
    }

    void CastSpell()
    {
            Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, _damageRange, _damageLayer);
        int magicToCast = 1;

            for (int i = 0; i < magicToCast; i++)
            {
                if (enemiesInRange[i].gameObject.CompareTag("Player") && _canCast)
                {
                    Vector3 spawnposition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                    Vector2 direction = enemiesInRange[i].gameObject.transform.position - spawnposition;

                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    //
                    {
                        GameObject magic = Instantiate(_magicPrefab, spawnposition, Quaternion.identity);
                        magic.transform.rotation = Quaternion.Euler(0, 0, angle + 120);
                    StartCoroutine(RotateToTarget(magic.transform.rotation.eulerAngles.z, magic.transform.rotation.eulerAngles.z - 45, magic));
                        //   magic.transform.localScale = new Vector3(1, scaleX, 1);

                    }
                }
            }
            StartCoroutine(Cooldown());

    }


    IEnumerator RotateToTarget(float startAngle, float targetAngle,GameObject magic)
    {
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // 0-1 arasý normalleþtir

            // Smoothstep uygularsak daha doðal olur:
            t = t * t * (3f - 2f * t);

            float currentAngle = Mathf.LerpAngle(startAngle, targetAngle, t);
            magic.transform.rotation = Quaternion.Euler(0, 0, currentAngle);

            yield return null;
        }

        // Tam hedef açýya sabitle
        magic.transform.rotation = Quaternion.Euler(0, 0, targetAngle);

    }

    private IEnumerator Cooldown()
    {
        _canCast = false;
        yield return new WaitForSeconds(_cooldown);
        _canCast = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _damageRange);
    }
}
