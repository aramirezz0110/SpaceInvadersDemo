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
    public Button level1Button;
    public Button level2Button;
    public Button creditsButton;
    [Header("UI Credits Screen")]
    public GameObject creditsScreenPanel;
    public Button backToSelectorScreen;

    #endregion
    #region Unity Methods
    private void Start()
    {
        ActivatePanel(selectorScreenPanel.name);

        level1Button.onClick.AddListener(()=>LoadScene(1));
        level2Button.onClick.AddListener(()=>LoadScene(2));
        creditsButton.onClick.AddListener(() => ActivatePanel(creditsScreenPanel.name));

        backToSelectorScreen.onClick.AddListener(()=>ActivatePanel(selectorScreenPanel.name));
    }
    #endregion

    #region Private Methods
    private void ActivatePanel(string name)
    {
        selectorScreenPanel.SetActive(selectorScreenPanel.name.Equals(name));
        creditsScreenPanel.SetActive(creditsScreenPanel.name.Equals(name));
    }
    private void LoadScene(int index)
    {
        PersistentData.Instance.levelSelected = index;
        SceneSwitcherManager.Instance.LoadGameScene();
    }
    
    #endregion
}
