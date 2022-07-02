using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(order =0, menuName ="Scriptables/Levels Settings",fileName ="New Levels Settings")]
public class LevelsSettings : ScriptableObject
{
    [SerializeField] private List<LevelSettings> levelsSettings = new List<LevelSettings>();
    public LevelSettings GetLevelSettings(int level)
    {
        LevelSettings tempLevelSetting= null;
        foreach(LevelSettings levelSetting in levelsSettings)
        {
            if(levelSetting.level == level)
            {
                tempLevelSetting = levelSetting;
                break;
            }
        }
        return tempLevelSetting;
    }
}
[System.Serializable]
public class LevelSettings
{
    public int level;
    public Sprite backGroundImage;
    [Header("Enemies speed")]
    public float greenEnemySpeed;
    public float blueEnemySpeed;
    public float redEnemySpeed;
    [Header("Enemies spawn wait time")]
    public float greenEnemySpawnWaitTime;    
    public float blueEnemySpawnWaitTime;    
    public float redEnemySpawnWaitTime;
    public float scaleFactor;
}
