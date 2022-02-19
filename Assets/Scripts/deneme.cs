using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.TouchPhase;

public class deneme : MonoBehaviour
{
    void Update()
    {
        foreach(Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Moved)
            {
                Debug.Log("you just touched the screen !");
                // Construct a ray from the current touch coordinates
                //Ray ray = Camera.main.ScreenPointToRay(touch.position);
                //if (Physics.Raycast(ray))
                //{
                //    Debug.Log("touch phase moved");
                //}
            }
        }
    }
}
