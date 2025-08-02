using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class GoldenCatController : MonoBehaviour
{
    public List<GameObject> activeCoins;
    public GameObject coinPrefab;
    public int amountOfCoins;
    public Vector3 spawnLocation;
    public List<int> goldenCoinCost;
    public int stage;
    public Animator anim;
    public Persistent p;
    public TextMeshProUGUI text;
    void Start()
    {
        p = FindAnyObjectByType<Persistent>();
        amountOfCoins = p.coin;
        stage = p.level;
    }
    void Update()
    {
        text.text = amountOfCoins + "/" + goldenCoinCost[stage];
    }

    public void spawnCoins(int amount)
    {
        amountOfCoins += amount;
        for (int i = 0; i < amount; i++)
        {
            GameObject coin = Instantiate(coinPrefab, spawnLocation, Quaternion.Euler(0, 0, 0));
            activeCoins.Add(coin);
        }
    }
    public void pushCoins()
    {
        for (int i = 0; i < activeCoins.Count; i++)
        {
            if (activeCoins[i] != null)
            {
                activeCoins[i].GetComponent<CoinBehaviour>().pushCoin();
            }
        }
    }
    public void checkGoldenCatBuy()
    {
        if (amountOfCoins >= goldenCoinCost[stage])
        {
            stage++;
            pushCoins();
            if (stage == goldenCoinCost.Count)
            {
                // do gameover thingy here
                return;
            }
            amountOfCoins -= goldenCoinCost[stage];
            anim.SetInteger("state", stage);
            p.coin = amountOfCoins;
            p.level = stage;
        }
        
    }
}
