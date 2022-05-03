using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Melon : Fruits
{
    private void Awake()
    {
        base.point = Random.Range(700, 1001);
    }
}
