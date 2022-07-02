using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    #region References 
    [Header("Levels Settings")]
    [SerializeField] private LevelsSettings levelsSettings;
    public LevelSettings levelSettings;
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
    [Header("Background References")]
    [SerializeField] private Image gameBackGround;

    #endregion

    #region Variables
    public static GameManager Instance;    
    [HideInInspector] public float horizontalLimit;
    [HideInInspector] public bool isPlayingGame;
    [HideInInspector] public bool stopSpawning;
    [HideInInspector] private bool gameOver;    
    [HideInInspector] public float inferiorLimit;
    [HideInInspector] public float superiorLimit;
    private int score = 0;
    #endregion   

    #region Unity Methods
    private void Awake()
    {
        Instance = this;
        levelSettings = levelsSettings.GetLevelSettings(PersistentData.Instance.levelSelected);
        ActivateCanvas(playerHUDCanvas.name);
    }
    private void Start()
    {
        isPlayingGame = true;

        pauseButton.onClick.AddListener(OnPauseButtonClicked);

        backToGameButton.onClick.AddListener(OnBackToGameButtonClicked);
        backToMainButton.onClick.AddListener(OnBackToMainButtonClicked);

        SetGameBackground();
        IncreaseScore(0);
        CalculateDistances();
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
        isPlayingGame = false;
        SaveScoreOnPlayerPrefs();

        TMP_Text tempText = backToGameButton.GetComponentInChildren<TMP_Text>();
        if (tempText != null)
        {
            tempText.text = "RETRY";
        }
        ActivateCanvas(gameStatusCanvas.name);
    }
    public bool IsMobilePlatform()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            return true;
        else
            return false;
    }
    #endregion

    #region Private Methods
    private void OnPauseButtonClicked()
    {
        Time.timeScale = 0;
        isPlayingGame = false;
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
    }
    private void ActivateCanvas(string canvasName)
    {
        playerHUDCanvas.SetActive(playerHUDCanvas.name.Equals(canvasName));
        gameStatusCanvas.SetActive(gameStatusCanvas.name.Equals(canvasName));
    }
    private void CalculateDistances()
    {
        inferiorLimit = GameObject.Find(GameTags.Player).transform.position.y;
        superiorLimit = GameObject.Find("EnemyOrigin").transform.position.y;
    }
    private void SetGameBackground()
    {
        gameBackGround.sprite = levelSettings.backGroundImage;
    }
    #endregion
}
