using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : GameObstacles
{
    [SerializeField] private Coin coinPrefab;
    [SerializeField] private int coinCount = 6;
    [SerializeField] private float spacing = 1;
    
    void Start()
    {
        SpawnLine();
    }


    private void SpawnLine()
    {
        for (int i = 0; i < coinCount; i++)
        {
            Vector3 pos = transform.position + Vector3.right * (i * spacing);
            Instantiate(coinPrefab, pos, Quaternion.identity, transform);
        }
    }

}
