using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBlockMovement : MonoBehaviour
{ // çalışmıyor lan NEDENNNNN
    private Touch touch;
    
    private Vector2 touchStartPosition;
    private Vector2 touchEndPosition;
    
    private float turnDegree = 90f;
    
    private bool turnsClockwise; // if slided left
    private bool turnsCounterclockwise; // if slided right
    private bool touchBegan;
    private bool touchEnded;
    private bool slideCoroutineStarted;
    
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
            if (!slideCoroutineStarted)
            {
                StartCoroutine(Slide());    
            }
            
        }
        else if (deltaPosition < 0)
        {
            turnsCounterclockwise = true;
            if (!slideCoroutineStarted)
            {
                StartCoroutine(Slide());    
            }
            
        }
        else
        {
            turnsClockwise = false;
            turnsCounterclockwise = false;    
        }
    }
 
    IEnumerator Slide()
    {
        slideCoroutineStarted = true;
        if (turnsClockwise && !turnsCounterclockwise)
        {
           for (int i = 0; i <= turnDegree; i++) // these places are probably where the bug occurs
           {
               transform.Rotate(0f,0f,  -1);
               yield return new WaitForSeconds(0.044444f); // be careful about the timing!
           } 
        } 
        else if (turnsCounterclockwise && !turnsClockwise)
        {
            for (int i = 0; i <= turnDegree; i++)
            {
                transform.Rotate(0f,0f,  1);
                yield return new WaitForSeconds(0.044444f);
            }
        } 
        else if (turnsClockwise && turnsCounterclockwise)
        {
            Debug.Log("ERROR CANNOT TURN BOTH WAYS");
        }

        slideCoroutineStarted = false;
    }

    IEnumerator DetectTouch() 
    {
        while (true)
        {
            if (Input.touchCount == 1)
            {
                touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    touchStartPosition = touch.position;
                    touchBegan = true;
                }
                else
                {
                    touchBegan = false;
                }
            
                
                if (touch.phase == TouchPhase.Ended)
                {
                    touchEndPosition = touch.position;
                    touchEnded = true;
                }
                else
                {
                    touchEnded = false;
                }

                if (touchBegan && touchEnded)
                {
                    CalculateSlide();
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
