using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MainMenuManager : MonoBehaviour
{
    #region References 
    [Header("UI Selector Screen")]
    public GameObject selectorScreenPanel;
    public TMP_Text highScoreText;
    public Button level1Button;
    public Button level2Button;
    public Button creditsButton;
    public Button configButton;
    [Header("UI Credits Screen")]
    public GameObject creditsScreenPanel;
    public Button backToSelectorScreen;
    [Header("Config Screen")]
    public GameObject configScreenPanel;
    public Button backToSelectorScreen2;
    public Button deleteHighScoreButton;
    #endregion
    #region Unity Methods
    private void Start()
    {
        ActivatePanel(selectorScreenPanel.name);
        GetHighScore();

        level1Button.onClick.AddListener(()=>LoadScene(1));
        level2Button.onClick.AddListener(()=>LoadScene(2));
        creditsButton.onClick.AddListener(() => ActivatePanel(creditsScreenPanel.name));
        configButton.onClick.AddListener(()=>ActivatePanel(configScreenPanel.name));

        backToSelectorScreen.onClick.AddListener(()=>ActivatePanel(selectorScreenPanel.name));   
        
        backToSelectorScreen2.onClick.AddListener(()=>ActivatePanel(selectorScreenPanel.name));
        deleteHighScoreButton.onClick.AddListener(DeleteHighScore);
    }
    #endregion

    #region Private Methods
    private void ActivatePanel(string name)
    {
        selectorScreenPanel.SetActive(selectorScreenPanel.name.Equals(name));
        creditsScreenPanel.SetActive(creditsScreenPanel.name.Equals(name));
        configScreenPanel.SetActive(configScreenPanel.name.Equals(name));
    }
    private void LoadScene(int index)
    {
        PersistentData.Instance.levelSelected = index;
        SceneSwitcherManager.Instance.LoadGameScene();
    }
    private void GetHighScore()
    {
        string tempScore = PlayerPrefs.GetInt(PlayerRefs.Score, 0).ToString();
        highScoreText.text = "HIGHSCORE: " + tempScore;
    }
    private void DeleteHighScore()
    {
        PlayerPrefs.SetInt(PlayerRefs.Score,0);
        highScoreText.text = "HIGHSCORE: " + 0;
    }
    #endregion
}
