using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : GameObstacles,IIntractable
{
    [SerializeField] GameObject visul;

    public void Onintraction()
    {
        Destroy(gameObject);
    }
}
