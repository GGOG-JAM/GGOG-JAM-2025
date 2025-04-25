using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class bossSkills : MonoBehaviour
{
    public Image image1,image2,image3;
    public Sprite[] slotSprites;


    public int coinFlipRes;


    public int[,] mineFarm = new int[3,3];
//    public int mineCount, winCount;
    
    public void CoinFlip() 
    {
        coinFlipRes = Random.Range(0, 2);
    }
    public void SpinSlot()
    {
        StartCoroutine(Spin());
    }
    IEnumerator Spin()
    {
        float duration = 2f;
        float timer = 0f;
        while (timer < duration) {
            image1.sprite = slotSprites[Random.Range(0, slotSprites.Length)];
            image2.sprite = slotSprites[Random.Range(0, slotSprites.Length)];
            image3.sprite = slotSprites[Random.Range(0, slotSprites.Length)];

            timer += 0.01f;
            yield return new WaitForSeconds(0.5f);
        }

    }

    public void SetUpMineFarm()
    {
        int col, sel;
        col = Random.Range(0, 3);
        sel = Random.Range(0, 3);

        for (int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if (i == col && j == sel)
                {
                    mineFarm[i, j] = 0;
                }
                else
                {
                    mineFarm[i, j] = 1;
                }

            }
        }
    }

}
