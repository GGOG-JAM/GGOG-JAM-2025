using UnityEngine;

public class MineAreas : MonoBehaviour
{

    private float timeInsideArea = 0f;
    public float openTime = 2f;
    private bool playerInside;
    public Sprite[] mineSprites;
    public int var;

    void OnTriggerEnter2D(Collider2D collision)
    {
        playerMain player = collision.gameObject.GetComponent<playerMain>();
        if (player != null)
        {
            playerInside = true;
            timeInsideArea = 0f;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        playerMain player = collision.gameObject.GetComponent<playerMain>();
        if (player != null)
        {
            playerInside = false;
            timeInsideArea = 0f;
        }
    }
    private void Update()
    {
        if (playerInside)
        {
            timeInsideArea += Time.deltaTime;
            if (timeInsideArea > openTime)
            {
                OpenArea();
                playerInside = false;
            }
        }
    }
    void OpenArea()
    {
        if (var == 0)
        {
            GetComponent<SpriteRenderer>().sprite = mineSprites[0];
        }
        else if (var == 1)
        {
            GetComponent<SpriteRenderer>().sprite = mineSprites[1];
        }
    }
}
