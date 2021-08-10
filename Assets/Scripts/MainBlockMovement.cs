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
    
    private int clockwiseTurnNo = -2;
    private int counterClockwiseTurnNo = 2;
    
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began) touchStartPosition = touch.position;
            
            if (touch.phase == TouchPhase.Ended) touchEndPosition = touch.position;
        }
        CalculateSlide();
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
            turnsClockwise = false;
            StartCoroutine(Slide());
        }
    }

    IEnumerator Slide()
    {
        if (turnsClockwise)
        {
           for (int i = 0; i <= turnDegree; i++) // these places are probably where the bug occurs
           {
               transform.Rotate(0f,0f,  clockwiseTurnNo);
               yield return new WaitForSeconds(0.02222222f);
           } 
        } 
        else if (turnsClockwise!)
        {
            for (int i = 0; i <= turnDegree; i++)
            {
                transform.Rotate(0f,0f,  counterClockwiseTurnNo);
                yield return new WaitForSeconds(0.02222222f);
            }
        }
        
    }
}
