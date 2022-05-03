using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class LoadingScene : MonoBehaviour
{

    //to controle the loading scene
    private bool anyKey = false;
    private AsyncOperation async;
    private bool isReady = false;
    //when scene is ready
    [SerializeField] private Text txtPressAnyKey;
    //progress bar image
    [SerializeField] private Image bgrBar;
    //scene information
    private int sceneIndex;
   
    private void Awake()
    {
       
        txtPressAnyKey.gameObject.SetActive(false);
    }
    void Start()
    {
        sceneIndex = PlayerPrefs.GetInt("CurrentScene", 0);
        ResetStat();
        loadNextScene();
    }
    //use for the main menu input
    public void OnAnyKey(InputAction.CallbackContext context)
    {
        anyKey = context.performed;
    }
   

    private void loadNextScene()
    {
        if (sceneIndex < 1)
        {
            var loadNextIndexScene = SceneManager.GetActiveScene().buildIndex + 1;
            async = SceneManager.LoadSceneAsync(loadNextIndexScene);
        }
        else
        {
            async = SceneManager.LoadSceneAsync(sceneIndex);
            isReady = true;
        }
        async.allowSceneActivation = false;
    }

    private void ResetStat()
    {
        Time.timeScale = 1f;
        Input.ResetInputAxes();
        System.GC.Collect();
    }
    void Update()
    {
        if (bgrBar)
            bgrBar.fillAmount = async.progress + 0.1f;

        if (async.progress > 0.89f && SplashScreen.isFinished )
        {
            txtPressAnyKey.gameObject.SetActive(true);

            if (anyKey)
            {
                isReady = true;
                anyKey = false;
            }
        }

        if (async.progress > 0.89f && SplashScreen.isFinished && isReady)
            async.allowSceneActivation = true;

    }



}
