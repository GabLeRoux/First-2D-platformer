using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JumpablePlatform : MonoBehaviour
{
    private Animator anim;
    private bool istouch = false;
    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }
    private void FixedUpdate()
    {
            anim.SetBool("isTouch", istouch);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            istouch = true;
            GetComponentInParent<AudioSource>().Play();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            istouch = false;
        }
    }



}
