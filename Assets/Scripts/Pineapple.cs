using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Pineapple : Fruits
{
    private void Awake()
    {
        base.point = Random.Range(100, 1000);
    }

}
