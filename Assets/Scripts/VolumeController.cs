using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private string nameParam;
    private float defaultValue = 0.35f;
    void Awake()
    {
        GetComponent<Slider>().value = defaultValue;
       
    }
    private void Start()
    {
        SetVolume(defaultValue);
    }

    public void SetVolume(float value)
    {
        mixer.SetFloat(nameParam, Mathf.Log10(value) * 30);
    }
}
