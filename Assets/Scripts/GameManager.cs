using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private String sceneName;
    
    public static bool inSceneA;
    public static bool inSceneB;
    
    private GameObject touchBorderObj;
    private Vector3 touchBorderWorldPos;
    private void OnEnable()
    {
        touchBorderObj = GameObject.Find("/CameraFollowingParent/touchBorder");
        touchBorderWorldPos.y = (Screen.height / 10) * 7;
    }

    private void Start()
    {
        FindScene();
        touchBorderObj.transform.Translate(touchBorderObj.transform.position.x,
            Camera.main.ScreenToWorldPoint(touchBorderWorldPos).y, touchBorderObj.transform.position.z);
    }

    private void FindScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (sceneName == "Scene_A")
        {
            inSceneA = true;
            inSceneB = false;
        }
        else if (sceneName == "Scene_B")
        {
            inSceneB = true;
            inSceneA = false;
        }
    }
    
}
