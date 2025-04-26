using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    public GameObject[] dices;
    public Transform player;
    public float travelTime = 1f;
    public float arcHeight = 2f;
    public float accelerationCurve = 2f; // Hýzlanma þiddeti (2 ideal)

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

            // Zaman ilerleyiþini hýzlandýrmak için t'yi kuvvetlendiriyoruz
            float t = Mathf.Clamp01(Mathf.Pow(elapsed / travelTime, accelerationCurve));

            // Doðrusal konum
            Vector3 linearPos = Vector3.Lerp(startPos, endPos, t);

            // Yayý ekle
            float heightOffset = arcHeight * Mathf.Sin(Mathf.PI * t);

            // Zarýn yeni pozisyonu
            dice.position = new Vector3(linearPos.x, linearPos.y + heightOffset, linearPos.z);

            yield return null;
        }

        // Ýstersen burada zar yere çarpýnca efekt verebilirsin
        CameraShake.Instance.Shake(0.3f, 0.2f);
    }
}