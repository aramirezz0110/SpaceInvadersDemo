using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    #region References 
    [Header("Player HUD Canvas")]
    [SerializeField] private GameObject playerHUDCanvas;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Button pauseButton;
    [Header("Game Status Canvas")]
    [SerializeField] private GameObject gameStatusCanvas;
    [SerializeField] private TMP_Text gameStateText;
    [SerializeField] private TMP_Text gameScoreText;
    [SerializeField] private Button backToGameButton;
    [SerializeField] private Button backToMainButton;
    #endregion

    #region Variables
    public static GameManager Instance;
    [SerializeField] private bool isPlayingGame;
    public bool stopSpawning;
    private bool gameOver;
    public float horizontalLimit;
    [SerializeField] private int score=0;
    #endregion   

    #region Unity Methods
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        pauseButton.onClick.AddListener(OnPauseButtonClicked);

        backToGameButton.onClick.AddListener(OnBackToGameButtonClicked);
        backToMainButton.onClick.AddListener(OnBackToMainButtonClicked);

        ActivateCanvas(playerHUDCanvas.name);
        IncreaseScore(0);
    }
    #endregion   
    #region Public Methods
    public void IncreaseScore(int value)
    {       
        score += value;
        scoreText.text = string.Format("Score: {0}",score);
    }
    public void SetGameOver()
    {
        gameStateText.text = "GAME OVER";
        gameScoreText.text = "SCORE: " + score;
        Time.timeScale = 0;
        stopSpawning = true;
        gameOver = true;
        SaveScoreOnPlayerPrefs();

        TMP_Text tempText = backToGameButton.GetComponentInChildren<TMP_Text>();
        if (tempText != null)
        {
            tempText.text = "RETRY";
        }
        ActivateCanvas(gameStatusCanvas.name);
    }
    #endregion

    #region Private Methods
    private void OnPauseButtonClicked()
    {
        Time.timeScale = 0;
        gameStateText.text = "PAUSE";
        gameScoreText.text = "SCORE: " + score;
        ActivateCanvas(gameStatusCanvas.name);
    }
    private void OnBackToGameButtonClicked()
    {        
        if (gameOver)
        {
            Time.timeScale = 1;
            SceneSwitcherManager.Instance.LoadGameScene();
        }
        else
        {
            Time.timeScale = 1;
            ActivateCanvas(playerHUDCanvas.name);
        }
        
    }
    private void OnBackToMainButtonClicked()
    {
        Time.timeScale = 1;
        SceneSwitcherManager.Instance.LoadMainMenuScene();
        SaveScoreOnPlayerPrefs();
    }
    private void SaveScoreOnPlayerPrefs()
    {
        PlayerPrefs.SetInt(PlayerRefs.Score, score);
        print("Score saved on PlayerPrefs, Score: "+ score);
    }
    private void ActivateCanvas(string canvasName)
    {
        playerHUDCanvas.SetActive(playerHUDCanvas.name.Equals(canvasName));
        gameStatusCanvas.SetActive(gameStatusCanvas.name.Equals(canvasName));
    }
    #endregion
}
