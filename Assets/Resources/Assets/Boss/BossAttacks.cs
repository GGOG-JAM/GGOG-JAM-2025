using System.Collections;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    public GameObject[] dices;
    public Transform player;
    public float travelTime = 1f;
    public float arcHeight = 2f;
    public float accelerationCurve = 2f;

    /*private void Start()
    {
        ThrowDice();
    }*/

    public void ThrowDice()
    {
        Transform dice = dices[0].transform;
        StartCoroutine(MoveDiceAlongCurve(dice));
    }

    private IEnumerator MoveDiceAlongCurve(Transform dice)
    {
        Vector3 startPos = dice.position;
        Vector3 endPos = player.position;
        float elapsed = 0f;

        while (elapsed < travelTime)
        {
            if (dice.position.y - player.position.y < 0)
            {

                elapsed += Time.deltaTime;

                float t = Mathf.Clamp01(Mathf.Pow(elapsed / travelTime, accelerationCurve));

                Vector3 linearPos = Vector3.Lerp(startPos, endPos, t);

                float heightOffset = arcHeight * Mathf.Sin(Mathf.PI * t);

                dice.position = new Vector3(linearPos.x + heightOffset, linearPos.y, linearPos.z);

                yield return null;
            }
            else
            {

                elapsed += Time.deltaTime;

                float t = Mathf.Clamp01(Mathf.Pow(elapsed / travelTime, accelerationCurve));

                Vector3 linearPos = Vector3.Lerp(startPos, endPos, t);

                float heightOffset = arcHeight * Mathf.Sin(Mathf.PI * t);

                dice.position = new Vector3(linearPos.x, linearPos.y + heightOffset, linearPos.z);

                yield return null;
            }
        }

        CameraShake.Instance.Shake(0.3f, 0.2f);
    }
}