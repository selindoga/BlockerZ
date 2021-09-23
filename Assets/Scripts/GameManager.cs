using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // eğer scene b de isem 
    private String sceneName;
    
    public static bool inSceneA;
    public static bool inSceneB;
    
    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (sceneName == "Scene A")
        {
            inSceneA = true;
            inSceneB = false;
        }
        else if (sceneName == "Scene B")
        {
            inSceneB = true;
            inSceneA = false;
        }
    }
    
    
    
    
}
