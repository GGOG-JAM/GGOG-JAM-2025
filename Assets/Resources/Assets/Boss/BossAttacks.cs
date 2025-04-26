using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    public GameObject[] dices;
    public Transform player;
    public float travelTime = 1f;
    public float arcHeight = 2f;
    public float accelerationCurve = 2f; // H�zlanma �iddeti (2 ideal)

    private void Start()
    {
        ThrowDice();
    }

    public void ThrowDice()
    {
        Transform dice = dices[0].transform;
        StartCoroutine(MoveDiceAlongCurve(dice));
    }

    private System.Collections.IEnumerator MoveDiceAlongCurve(Transform dice)
    {
        Vector3 startPos = dice.position;
        Vector3 endPos = player.position;
        float elapsed = 0f;

        while (elapsed < travelTime)
        {
            elapsed += Time.deltaTime;

            // Zaman ilerleyi�ini h�zland�rmak i�in t'yi kuvvetlendiriyoruz
            float t = Mathf.Clamp01(Mathf.Pow(elapsed / travelTime, accelerationCurve));

            // Do�rusal konum
            Vector3 linearPos = Vector3.Lerp(startPos, endPos, t);

            // Yay� ekle
            float heightOffset = arcHeight * Mathf.Sin(Mathf.PI * t);

            // Zar�n yeni pozisyonu
            dice.position = new Vector3(linearPos.x, linearPos.y + heightOffset, linearPos.z);

            yield return null;
        }

        // �stersen burada zar yere �arp�nca efekt verebilirsin
        CameraShake.Instance.Shake(0.3f, 0.2f);
    }
}