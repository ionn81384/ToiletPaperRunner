using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager instance = null;

    public const string version = "v.0.1";
    public Text versionText;

    [Header("Canvas")]
    public Canvas[] canvas;

    [Header("Camera")]
    public GameObject camera1;
    public GameObject camera2;

    [Header("Score")]
    public Text BestScore;
    public Text Score;

    [Header("Dead Text")]
    public Text bestScoreD;// "Best score \n +num"
    public Text scoreD;

    [Header("Canvas Menu")]
    public Text BestScoreMenu;// "Best score \n +num"
    public GameObject roller;
    public Text  rollerCount;

    public GameObject inputTutorial;
    public bool play = false;

    public GameObject leaderboardmenuobject;
    public GameObject mainMenuObject;

    [Header("Ads")]
    public GameObject button;
    public GameObject buttonRestorePurchases;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // If sucess then keep it
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        BestScoreMenu.text = "Best Score \n" + GameData.d.bestScore.ToString();
        BestScore.text =GameData.d.bestScore.ToString();
        Score.text = "0";
    }

    private void Update()
    {
        if (GameData.d.isReload)
        {
            clickPlay();
            GameData.d.isReload = false;
        }

        if (play)
        {
            if (Input.GetMouseButton(0))
            {
                inputTutorial.gameObject.SetActive(false);
                TerrainGenerator.instance.canMove = true;
                play = false;
            }
        }
    }

    private void FixedUpdate()
    {
        BestScoreMenu.text = "Best Score \n" + GameData.d.bestScore.ToString();
        BestScore.text = GameData.d.bestScore.ToString();
        Score.text = GameData.d.score.ToString();
        rollerCount.text = "Paper Collected " + GameData.d.toiletPaper.ToString();
        if (bestScoreD.gameObject.active)
        {
            bestScoreD.text = GameData.d.bestScore.ToString();
            scoreD.text = GameData.d.score.ToString();
        }
        
    }
    public void DisableAllCanv()
    {
        for (int i = 0; i < canvas.Length; i++)
        {
            canvas[i].gameObject.SetActive(false);
        }
    }

    public void clickPlay()
    {
        camera1.SetActive(false);
        camera2.SetActive(true);

        // Switch canvas
        DisableAllCanv();
        canvas[1].gameObject.SetActive(true);
        inputTutorial.gameObject.SetActive(true);
        play = true;

        AudioManager.instance.changeMusic(1);
    }

    public void clickToMenu()
    {
        //GameData.d.isReload = false;
        GameData.instance.SaveGame();
        //SceneManager.LoadScene(0);

        camera1.SetActive(true);
        camera2.SetActive(false);
        play = false;

        DisableAllCanv();
        canvas[0].gameObject.SetActive(true);

        //DisableAllCanv();
        //canvas[0].gameObject.SetActive(true);
        //leaderboardmenuobject.GetComponent<LeaderboardMenu>().ClearLeaderboard();

        AudioManager.instance.changeMusic(0);
    }

    public void clickToReplay()
    {
        //GameData.d.isReload = true;
        GameData.instance.SaveGame();
        scoreD.text = "0";
        //SceneManager.LoadScene(0);
        clickPlay();

        AudioManager.instance.changeMusic(1);
    }

    public void clickShowHideRoller()
    {
        roller.SetActive(!roller.active);
    }

    public void LeaderBoardButton()
    {
        if (FacebookAndPlayFabManager.Instance.IsLoggedOnFacebook)
        {
            // show leaderboard
            DisableAllCanv();
            canvas[4].gameObject.SetActive(true);
            leaderboardmenuobject.GetComponent<LeaderboardMenu>().Load();
        }
        else
        {
            // do login first
            DisableAllCanv();
            canvas[3].gameObject.SetActive(true);
        }

    }

    public void returnLogin()
    {
        DisableAllCanv();
        canvas[0].gameObject.SetActive(true);
    }
   
    public void ShowCanvasDead()
    {
        DisableAllCanv();
        canvas[2].gameObject.SetActive(true);
    }

    public void PublishBigScore()
    {
        mainMenuObject.GetComponent<MainMenu>().PostScoreOnPlayFab(GameData.d.bestScore.ToString());
    }

    public void clickTwitter()
    {
        Application.OpenURL("https://twitter.com/PencilGamesStd");
    }

    public void clickInstagram()
    {
        Application.OpenURL("https://www.instagram.com/pencilgamesstudio/?hl=pt");
    }

}
