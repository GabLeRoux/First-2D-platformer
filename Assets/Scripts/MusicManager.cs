using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicManager : MonoBehaviour
{
    public static MusicManager instance = null;
    [SerializeField]private bool NeverStop = false;
    private void Awake()
    {
        #region instance
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this) 
        { 
            Destroy(gameObject); 
        }
        #endregion
    }
    void Update()
    {
        if (NeverStop)
        {
            Object.DontDestroyOnLoad(gameObject);
        }
    }
}
