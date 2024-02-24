using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    #region Fields
    /// <summary>
    /// Singleton
    /// </summary>
    public static GameHandler Instance { get; private set; }

    /// <summary>
    /// Score Textini tutan degisken
    /// </summary>
    public TMPro.TextMeshProUGUI ScoreText;

    /// <summary>
    ///Finish panelindeki Score Textini tutan degisken
    /// </summary>
    public TMPro.TextMeshProUGUI FinishPanelScoreText;

    /// <summary>
    ///Finish panelindeki Best Score Textini tutan degisken
    /// </summary>
    public TMPro.TextMeshProUGUI FinishPanelBestScoreText;
   
    /// <summary>
    /// Score Degiskeni
    /// </summary>
    public int score = 0;

    /// <summary>
    /// Oldu mu kontrol degiskeni
    /// </summary>
    public bool isDead = false;

    /// <summary>
    ///Finish panelini tutan degisken
    /// </summary>
    public GameObject FinishPanel;
    #endregion

    #region Methods
    /// <summary>
    /// Singleton atamalari yapilan kisim
    /// </summary>
    private void Awake()
    {

        if (Instance != null && Instance != this)
            Destroy(this);

        else
            Instance = this;

    }

    /// <summary>
    /// Olum kontrolu yapilan Update methodu
    /// </summary>
    private void Update()
    {
        if (isDead)
        {
            PlayerDead();
        }
    }
   
    /// <summary>
    /// Score Arttirma Methodu
    /// </summary>
    public void AddScore()
    {
        score++;
        if (ScoreText != null)
            ScoreText.text = "Score: " + score;
    }
   
    /// <summary>
    /// Player Olum methodu
    /// </summary>
    public void PlayerDead()
    {
        Time.timeScale = 0;
        FinishPanel.SetActive(true);
        FinishPanelScoreText.text=score.ToString();
        if (PlayerPrefs.GetInt("BestScore",0)<score)
        {
            PlayerPrefs.SetInt("BestScore", score);
            FinishPanelBestScoreText.color = Color.green;
        }
        FinishPanelBestScoreText.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
    }

    /// <summary>
    /// Restart Buttonu methodu
    /// </summary>
    public void RestartGame()
    {
        Time.timeScale = 1;
        isDead = false;
        SceneManager.LoadScene("GameScene");
    }
    #endregion
}
