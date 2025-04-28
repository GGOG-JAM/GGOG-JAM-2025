using System.Collections;
using UnityEngine;

public class TriggerMainEvent : MonoBehaviour
{
    public Camera cam;
    public GameObject boss;


    public float targetSize = 20f;
    public float zoomDuration = 1.5f; // Ka� saniyede tam b�y�yecek

    private bool hasZoomed = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<statSystemForPlayer>() != null)
        {
            StartCoroutine(ZoomCamera());
            hasZoomed = true;
            boss.SetActive(true);
        }
    }

    IEnumerator ZoomCamera()
    {
        float startSize = cam.orthographicSize;
        float timeElapsed = 0f;

        while (timeElapsed < zoomDuration)
        {
            cam.orthographicSize = Mathf.Lerp(startSize, targetSize, timeElapsed / zoomDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        cam.orthographicSize = targetSize; // Son de�eri sabitle
    }
}
