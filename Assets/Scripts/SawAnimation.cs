using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawAnimation : MonoBehaviour
{
    private Vector3 startPos;
    private const float rotSpeed = 250.00f;
    [Range(4f, 15f)] [SerializeField] private float posSpeed = 4.75f;
    [Range(1f, 10f)] [SerializeField] private float maxDistance = 5f;
    [Header("StartSide")] [SerializeField]
    private bool isLeft = true;
    [SerializeField] private bool isAxisX = true;
    void Start()
    {
        startPos = transform.position;
    }
    private void Awake()
    {
        gameObject.tag = "Enemie";
    }

    void Update()
    {
        if (isAxisX)
        {
            if (isLeft)
            {
                SawMove(posSpeed, rotSpeed);
                if (transform.position.x > (startPos.x + maxDistance))
                {
                    isLeft = false;
                }
            }
            else if (!isLeft)
            {
                SawMove(-posSpeed, -rotSpeed);
                if (transform.position.x < (startPos.x - maxDistance))
                {
                    isLeft = true;
                }
            }
        }
        else if (!isAxisX)
        {
            if (isLeft)
            {
                SawMoveUpDown(posSpeed, rotSpeed);
                if (transform.position.y > (startPos.y + maxDistance))
                {
                    isLeft = false;
                }
            }
            else if (!isLeft)
            {
                SawMoveUpDown(-posSpeed, -rotSpeed);
                if (transform.position.y < (startPos.y - maxDistance))
                {
                    isLeft = true;
                }
            }
        }
    } 
        
    
    private void SawMove(float posSpeed, float rotSpeed)
    {
        transform.position += (Vector3.right * posSpeed * Time.deltaTime);
        transform.Rotate(new Vector3(0, 0, 4) * rotSpeed * Time.deltaTime, Space.Self);
    }
    private void SawMoveUpDown(float posSpeed, float rotSpeed)
    {
        transform.position += (Vector3.up * posSpeed * Time.deltaTime);
        transform.Rotate(new Vector3(0, 0, 4) * rotSpeed * Time.deltaTime, Space.Self);
    }
}
