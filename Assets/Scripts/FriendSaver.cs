using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendSaver : MonoBehaviour
{
    [SerializeField] private GameObject[] Enemies;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach(var pic in Enemies)
            {
                pic.SetActive(false);
            }
            Destroy(gameObject);
        }
    }

}
