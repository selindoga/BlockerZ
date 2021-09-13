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

    
    private IEnumerator TurnPlatform()
    {
        //Sequence sequence = DOTween.Sequence();
        
        if (swipedLeft && !swipedRight)
        {
            Tween tween = transform.DORotate(new Vector3(0, 0, -90f), .6f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear);
            yield return tween.WaitForCompletion();
            swipedLeft = false;
            swipedRight = false;
        }
        
        else if (swipedRight && !swipedLeft)
        {
            Tween tween = transform.DORotate(new Vector3(0, 0, +90f), .6f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear); 
            yield return tween.WaitForCompletion();
            swipedRight = false;
            swipedLeft = false;
        }
        
    }

    public IEnumerator getTurnPlatform()
    {
        Debug.Log("in the getTurnPlatform, should start the coroutine");
        return TurnPlatform();
    }

}
