using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBlockMovement : MonoBehaviour
{
    private Touch touch;
    
    private Vector2 touchStartPosition;
    private Vector2 touchEndPosition;
    
    private float turnDegree = 90f;
    private bool turnsClockwise; // if slided left
    private bool turnsCounterclockwise; // if slided right
    
    private int clockwiseTurnNo = -1;
    private int counterClockwiseTurnNo = 1;
    
    private void Start()
    {
        StartCoroutine(DetectTouch());
    }

    private void CalculateSlide()
    {
        float deltaPosition;
        deltaPosition = touchStartPosition.x - touchEndPosition.x;
        if (deltaPosition > 0) // turn clockwise, slided left, rotation.z - 90 çıkar
        {
            turnsClockwise = true;
            StartCoroutine(Slide());
        }
        else if (deltaPosition < 0)
        {
            turnsCounterclockwise = true;
            StartCoroutine(Slide());
        }

        turnsClockwise = false;
        turnsCounterclockwise = false;
        
    }
 
    IEnumerator Slide()
    {
        if (turnsClockwise && !turnsCounterclockwise)
        {
           for (int i = 0; i <= turnDegree; i++) // these places are probably where the bug occurs
           {
               transform.Rotate(0f,0f,  clockwiseTurnNo);
               yield return new WaitForSeconds(0.02222222222f);
           } 
        } 
        else if (turnsCounterclockwise && !turnsClockwise)
        {
            for (int i = 0; i <= turnDegree; i++)
            {
                transform.Rotate(0f,0f,  counterClockwiseTurnNo);
                yield return new WaitForSeconds(0.02222222222f);
            }
        }
    }

    IEnumerator DetectTouch()
    {
        while (true)
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
            
                if (touch.phase == TouchPhase.Began) touchStartPosition = touch.position;
            
                if (touch.phase == TouchPhase.Ended) touchEndPosition = touch.position;
            
                CalculateSlide();
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
