using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcherManager : MonoBehaviour
{    
    public void LoadMainMenuScene()
    {
        LoadScene("0-MainMenuScene");
    }
    public void LoadGameScene()
    {
        LoadScene("1-GameScene");
    }
    private void LoadScene(string value)
    {
        SceneManager.LoadScene(value);
    }
}
