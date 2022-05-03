using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : MonoBehaviour
{
    private Vector3 startRotate;
    private const float spikeRotationSpeed = 250f;
    [Range(70f,85f)][SerializeField] private float chaineRotationSpeed = 80f;
    [SerializeField] private bool isLeft = false;
    [SerializeField] private GameObject spikeBall;
   
    private void Awake()
    {
        startRotate = transform.rotation.eulerAngles;
        spikeBall.tag = "Enemie";
    }

    
    void Update()
    {
       // print(startRotate);
      //  print(transform.rotation.eulerAngles);
        if (isLeft)
        {
            SpikeMove(spikeRotationSpeed, chaineRotationSpeed);
            
            if (transform.rotation.eulerAngles.z < (startRotate.z + 90f)) 
            {
                Invoke("ChangeSideToLeft", 1.00f);
            }
        }
        if (!isLeft)
        {
            SpikeMove(-spikeRotationSpeed, -chaineRotationSpeed);
            if (transform.rotation.eulerAngles.z > (startRotate.z + 200f) )
            {
                Invoke("ChangeSideToRight", 1.00f);
            }
        }
    }
    private void SpikeMove(float spikeRotationSpeed, float rotationSpeed)
    {
        spikeBall.transform.Rotate(Vector3.forward * spikeRotationSpeed * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.forward  * rotationSpeed * Time.deltaTime, Space.Self);
    }

    private void ChangeSideToLeft()
    {
        isLeft = false;
    }

    private void ChangeSideToRight()
    {
        isLeft = true;
    }

}
