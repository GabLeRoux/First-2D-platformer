using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //instance
    public static GameManager instance = null;
    //player variable to manipulate lifes
    [SerializeField]private PlayerBehaviour playerBehaviour;
    //lifes
    private List<Image> array_Images = new List<Image>();
    [SerializeField] private Image prefab_Live;
    [SerializeField] private GameObject panel_Lives;
    private bool LiveSet = false;
    //score
    [SerializeField] private Text score;
    private const string scoreTxt = "Score: ";
    private int actualPoint = 0;
    int tempPoint = 0;
    //high score
    [SerializeField] private Text highScore;
    private const string highScoreTxt = "High Score: ";
    //ScoreReach
    [SerializeField] private Text finalScore;
    private const string finalScoretxt =  "Final Score: ";
    //LevelPass message
    [SerializeField] private Text LevelPass_Message;
    
    //GameOver
    [SerializeField] private Text gameOver_Message;
    //particules effects
    [SerializeField] private ParticleSystem[] Array_Particule;
   
   //button and panel
    [SerializeField] private Button[] array_btn_menu; //[0] tryagain , [1] continue,[2] option , [3] nextLevel
    [SerializeField] private GameObject panel_menu;

    // game status
    private bool isNewGame = true;
    private bool isOver;

    //sound 
    private new AudioSource audio;
    [SerializeField] private AudioClip[] playerSFX; // [0] collected , [1] hit , [2] gameover, [3]winner

    public bool IsOver { get => isOver;}

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
        ActualizeScore();
        SetUIActivation();
        SetPlayerLifes();
    }
    private void SetUIActivation()
    {
        isOver = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //disable button and temp message 
        LevelPass_Message.gameObject.SetActive(false);
        gameOver_Message.gameObject.SetActive(false);
        array_btn_menu[0].gameObject.SetActive(false); //tryagain
        array_btn_menu[3].gameObject.SetActive(false); //next level
        highScore.gameObject.SetActive(false);
        finalScore.gameObject.SetActive(false);
        //set audio too
        audio = GetComponent<AudioSource>();
    }

    #region Life parameter
    private void CreateLifes(int number)
    {
        for (int i = 0; i < number; i++)
        {
            AddLife();
        }
    }
    private void SetPlayerLifes()
    {
        if (isNewGame)
        {
            CreateLifes(3);
            LiveSet = true;
        }
        #region if add level
        //var Lifes = PlayerPrefs.GetInt("PlayerLifes");
        //else if(!isNewGame && Lifes > 0)
        //{
        //    CreateLifes(Lifes);
        //}
        #endregion
    }

    private void LoosePoint()
    {
        var pointLost = Random.Range(100, 1000);
        actualPoint -= pointLost;
        tempPoint -= pointLost;
    }

   
    

    public bool PlayerTouch(int actualLive, Vector3 pos, bool instakill = false )
    {
        array_Images[actualLive - 1].fillAmount -= 0.50f;
        if (instakill)
            array_Images[actualLive - 1].fillAmount = 0f;
        LoosePoint();
        ActualizeScore();
        SetSoundBehaviour(1);


        //dont play if game is over
        if (playerBehaviour.PlayerLives > 0)
        {
            Array_Particule[1].transform.position = pos;
            Array_Particule[1].Play();
        }

        if (array_Images[actualLive - 1].fillAmount == 0.00f)
        {
            Destroy(array_Images[actualLive - 1].gameObject);
            array_Images.RemoveAt(actualLive - 1);
            playerBehaviour.PlayerLives--;
            if (playerBehaviour.PlayerLives == 0) GameOver();
            return true;
        }
        return false;
    }


    
    public void AddLife()
    {
        //adding the new life
        Image ImageDuplication = Instantiate(prefab_Live, panel_Lives.transform);
        array_Images.Add(ImageDuplication);
        //reorganise Lifes
        int liveNumber = array_Images.Count; 
        if (LiveSet)
        {
            float tempAmount = array_Images[liveNumber - 2].fillAmount;
            if (tempAmount != 1.00f)
            {
                array_Images[liveNumber - 2].fillAmount = 1.00f;
                array_Images[liveNumber - 1].fillAmount = tempAmount;
            }
        }
        playerBehaviour.PlayerLives++;
    }
    #endregion
    private void SetSoundBehaviour(int index)
    {
        audio.clip = playerSFX[index];
        audio.Play();
    }
    public void LevelWin()
    {
        SetSoundBehaviour(3);
        InputBehaviour();
        ScoreMessage();
        array_btn_menu[3].gameObject.SetActive(true); //next level
        LevelPass_Message.gameObject.SetActive(true);
        PlayerPrefs.SetInt("CurrentScene", SceneManager.GetActiveScene().buildIndex);

        #region if adding another level
        //var IncreaseLevel = SceneManager.GetActiveScene().buildIndex + 1;
        //PlayerPrefs.SetInt("PlayerLifes", playerBehaviour.PlayerLives);
        //isNewGame = false;
        #endregion
    }
    public void GameOver()
    {
        SetSoundBehaviour(2);
        InputBehaviour();
        ScoreMessage();
        gameOver_Message.gameObject.SetActive(true);
        array_btn_menu[0].gameObject.SetActive(true); // try again
    }
    private void InputBehaviour()
    {
        this.isOver = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0f;
        InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
        panel_menu.gameObject.SetActive(true);
        array_btn_menu[1].GetComponentInChildren<Button>().interactable = false;
        array_btn_menu[2].GetComponentInChildren<Button>().interactable = false;
    }
    private void ScoreMessage()
    {
        if (actualPoint > PlayerPrefs.GetInt("HighScore", 0))
        {
            highScore.gameObject.SetActive(true);
            highScore.text = highScoreTxt + actualPoint.ToString();
            PlayerPrefs.SetInt("HighScore", actualPoint);
        }
        else 
        {
            finalScore.gameObject.SetActive(true);
            finalScore.text = finalScoretxt + actualPoint.ToString();
        }
    }


   #region Score fonction
    public void AddPoints(int point, Vector3 pos)
    {
        SetSoundBehaviour(0);
        Array_Particule[0].transform.position = pos;
        Array_Particule[0].Play();
        actualPoint += point;
        tempPoint += point;
        ActualizeScore();
        if (tempPoint >= 25000)
        {
            AddLife();
            tempPoint = 0;
        }
    }
  
    private void ActualizeScore()
    {
        score.text = scoreTxt + actualPoint.ToString("D8");
    }
    #endregion

    
}
