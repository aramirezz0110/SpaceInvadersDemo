using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    #region References 
    [SerializeField] private TMP_Text scoreText;
    #endregion

    #region Variables
    public static GameManager Instance;
    [SerializeField] private bool isPlayingGame;
    public bool stopSpawning;
    public float horizontalLimit;
    [SerializeField] private int score;
    #endregion   

    #region Unity Methods
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    #region Public Methods
    public void IncreaseScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }
    #endregion
    #region Private Methods

    #endregion
}
