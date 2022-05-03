using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehaviour : MonoBehaviour
{
    [SerializeField] private float maxDistance = 1f;
    [SerializeField] private float blockSpeed = 1f;
    [SerializeField] private GameObject oneShot;
    private Vector3 startPos;
    private bool touchGround = false;
    private bool isTrigger = false;
    private int count = 0;
    public bool IsTrigger { get => isTrigger; set => isTrigger = value; }

    private void Awake()
    {
        startPos = transform.position;
        gameObject.tag = "Enemie";
    }

    private void AxisYMove(float blockSpeed)
    {
        transform.position += (Vector3.up * blockSpeed * Time.deltaTime);
        oneShot.transform.position += (Vector3.up * blockSpeed * Time.deltaTime);
    }
    private void Update()
    {
        if (isTrigger)
        {
            if (!touchGround)
            {
                AxisYMove(-blockSpeed);
                if (transform.position.y < (startPos.y - maxDistance))
                {
                    StartCoroutine(Timer());
                }
            }
            else if (touchGround && count > 0)
            {
                AxisYMove(blockSpeed);
                if (transform.position.y > startPos.y )
                {
                    this.touchGround = false;
                    this.isTrigger = false;
                    count = 0;
                }
            }
        }
         
    }
    private IEnumerator Timer()
    {
        this.touchGround = true;
        yield return new WaitForSeconds(1.5f);
        count++;
      
    }
}



