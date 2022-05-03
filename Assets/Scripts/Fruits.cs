using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Fruits : MonoBehaviour
{
    protected int point = 10;
    protected GameManager instance;
    
    private void Start()
    {
        instance = GameManager.instance;
        gameObject.tag = "Fruit";
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerCollected"))
        {
            instance.AddPoints(point,transform.position);
            Destroy(gameObject);
        }
    }
    
   
}
