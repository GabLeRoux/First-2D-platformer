using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Orange : Fruits
{
    private void Awake()
    {
        base.point = 2000;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerCollected"))
        {
            base.OnTriggerEnter2D(collision);
            base.instance.AddLife();

        }
    }




}
