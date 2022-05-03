using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    public static PanelController instance;

    [SerializeField] private GameObject[] array_Panel;
    [SerializeField] private Selectable[] array_selectable;

    IEnumerator Start()
    {
        yield return new WaitForFixedUpdate();
        
    }
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
    

    public void PannelSwitch(int index)
    {
        for (int i = 0; i < array_Panel.Length; i++)
        {
            array_Panel[i].SetActive(index == i);
            if (index == i)
            {
                array_selectable[i].Select();
            }
        }
    }
    public void PauseMenu()
    {
        InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        int activePanel = 0;
        //decrease panel by one if there a panel active
            for (int i = 0; i < array_Panel.Length; i++)
            {
                if (array_Panel[0].activeSelf )
                {
                    ContinueButton();
                activePanel++;
                    break;
                }else
                if (array_Panel[i].activeSelf)
                 {
                    activePanel++;
                    array_Panel[i].SetActive(false);
                    array_Panel[i-1].SetActive(true);
                    break;
                 }
            }
        //OpenMain Menu if no panel is active
            if (activePanel == 0)
            {
            Time.timeScale = 0f;
                PannelSwitch(0);
                print(activePanel);
            }
    }
    public void ExitButton()
    {
        //when player quit rest the variable to default
        PlayerPrefs.SetInt("CurrentScene", 0);
        Application.Quit();
    }
    public void ContinueButton()
    {
        foreach(var panel in array_Panel)
        {
            panel.SetActive(false);
        }
        Time.timeScale = 1f;
        InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    //fonction for button try again and next level
    public void SetLoadingScene() //use for levelpass and try again  ** index level is set in GameManager
    {
        //the level it allready set it the game manager
        SceneManager.LoadScene("Loading");
    }

   
    public void NewGameButton()
    {
        //set the first level
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.SetInt("CurrentScene", 2);
        Scene_Loading();
    }

    //for main menu
    public void OnReturnControl(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PannelSwitch(0);
        }
    }

    private void Scene_Loading()
    {
        SceneManager.LoadScene("Loading");
    }
}
