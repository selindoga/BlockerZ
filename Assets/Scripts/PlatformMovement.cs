using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlatformMovement : MonoBehaviour
{
    private Touch touch;
    
    private Vector2 touchStartPosition;
    private Vector2 touchEndPosition;

    private bool turnsClockwise; // if slided left
    private bool turnsCounterclockwise; // if slided right
    private bool touchBegan;
    private bool touchEnded;
    private bool slideCoroutineStarted;
    
    public static bool swipedLeft;
    public static bool swipedRight;
    

    private IEnumerator TurnPlatform() // :D
    {
        
        if (swipedLeft)
        {
            transform.DORotate(new Vector3(0, 0, -90f), 1f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).WaitForCompletion();
            yield return null;

            
            /* tween bitmeden diğer tweenleri başlatma
             çünkü tam 180 e yada 90 a gitmemiş oluyor en son ki tween bitince rotation eksik kalmış oluyor 
             
             diğer tween başlatılmak istense bile şu anki tweenin bitmesini bekle
             */

            swipedLeft = false;
        }
        
        else if (swipedRight)
        {
            transform.DORotate(new Vector3(0, 0, +90f), 1f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).WaitForCompletion(); // RotateMode.LocalAxisAdd
            yield return null;
            
            swipedRight = false;
        }
        
    }

    public IEnumerator getTurnPlatform()
    {
        Debug.Log("in the getTurnPlatform, should start the coroutine");
        return TurnPlatform();
    }

}
