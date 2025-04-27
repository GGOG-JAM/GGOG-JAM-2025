using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class bossSkills : MonoBehaviour
{
    statSystem statSystem;

    public GameObject[] slotObjes;
    public Sprite[] slotSprites;


    Animator coinAnim;
    public GameObject coinF;
    public int coinFlipRes;
    public float animDuration = 1f;
    public Sprite[] flipSprites;


    public int[,] mineFarm = new int[3,3];
    //    public int mineCount, winCount;
    public GameObject[] mineFarmObjSolo;
    public GameObject[,] mineFarmObj = new GameObject[3,3];



    private void Start()
    {
        statSystem = GetComponent<statSystem>();
    }




    public void CoinFlip() 
    {
        coinF.SetActive(true);
        coinAnim = coinF.GetComponent<Animator>();
        StartCoroutine(FlipRoutine());

        
    }

    IEnumerator FlipRoutine()
    {
        for (int i = 0; i < 2; i++)
        {
            coinAnim.enabled = true;
            yield return new WaitForSeconds(animDuration);
        }
        coinAnim.enabled = false;
        coinFlipRes = Random.Range(0, 2);
        if (coinFlipRes == 0)
        {
            coinF.GetComponent<SpriteRenderer>().sprite = flipSprites[0];
        }
        else
        {
            coinF.GetComponent<SpriteRenderer>().sprite = flipSprites[1];
        }
    }

        public void SpinSlot()
    {
        slotObjes[0].SetActive(true);
        StartCoroutine(Spin());
    }
    IEnumerator Spin()
    {
        float duration = 1.2f;
        float timer = 0f;
        while (timer < duration) {
            slotObjes[1].GetComponent<SpriteRenderer>().sprite = slotSprites[Random.Range(0, slotSprites.Length)];
            slotObjes[2].GetComponent<SpriteRenderer>().sprite = slotSprites[Random.Range(0, slotSprites.Length)];
            slotObjes[3].GetComponent<SpriteRenderer>().sprite = slotSprites[Random.Range(0, slotSprites.Length)];

            timer += 0.1f;
            yield return new WaitForSeconds(0.3f);
        }

    }

    public void PlayMineFarm()
    {
        Debug.Log("Farm Baþlsýn");
        SetUpMineObjs();
        SetUpMineFarm();
    }

    void SetUpMineObjs()
    {
        int hand = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                mineFarmObj[i, j] = mineFarmObjSolo[hand];
                hand++;
            }
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
                    mineFarmObj[i, j].GetComponent<MineAreas>().var = 0; 
                }
                else
                {
                    mineFarm[i, j] = 1;
                    mineFarmObj[i, j].GetComponent<MineAreas>().var = 1;
                }

            }
        }
    }

}
