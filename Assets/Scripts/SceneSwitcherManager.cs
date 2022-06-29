using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneSwitcherManager : MonoBehaviour
{
    public static SceneSwitcherManager Instance;
    #region Unity Methods
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    #region Public methods
    public void LoadMainMenuScene()
    {
        LoadScene(SceneRefs.MainMenu);
    }
    public void LoadGameScene()
    {
        LoadScene(SceneRefs.GameScene);
    }
    #endregion

    #region Private methods 
    private void LoadScene(string value)
    {
        SceneManager.LoadScene(value);
    }
    #endregion
}
