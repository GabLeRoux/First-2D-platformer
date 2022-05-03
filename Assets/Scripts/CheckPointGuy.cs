using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointGuy : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            NewCheckPoint();
            anim.SetTrigger("touch");
            Destroy(gameObject,0.4f);
            GetComponent<AudioSource>().Play();
        }
    }
    private void NewCheckPoint()
    {
        PlayerPrefs.SetFloat("CKPositionX", transform.position.x);
        PlayerPrefs.SetFloat("CKPositionY", transform.position.y);
    }
}
