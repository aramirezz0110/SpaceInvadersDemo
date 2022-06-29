using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    public static PersistentData Instance;
    private int _levelSelected;
    public int levelSelected 
    { 
        get { return _levelSelected; } 
        set 
        { 
            _levelSelected = value;
            print(_levelSelected);
        }  
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance!=this)
        {
            Destroy(gameObject);
        }
    }
    
    
}
