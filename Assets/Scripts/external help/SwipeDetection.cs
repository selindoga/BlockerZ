using System;
using System.Collections;
using UnityEngine;

// This code was copied from Samyam's video tutorial
// The code was edited and new codes were added by me

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private float minimumDistance = .2f;
    [SerializeField] private float maximumTime = 1f;
    [SerializeField, Range(0f, 1f)] private float directionThreshold = .9f;
    
    private InputManager inputManager;

    private Vector2 startPosition;
    private Vector2 endPosition;
    private float startTime;
    private float endTime;

    private GameObject platformGameObject;
    
    private void Awake()
    {
        inputManager = InputManager.Instance;
        platformGameObject = GameObject.Find("Platform");
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
    }
    
    private void SwipeEnd(Vector2 position, float time)
    {
        
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector3.Distance(startPosition, endPosition) >= minimumDistance && (endTime - startTime) <= maximumTime)
        {
            Debug.DrawLine(startPosition, endPosition, Color.red, 5f); 
            Vector3 direction = endPosition - startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2D);
        }
    } // yazamadım: startPosition .y uzunluk ekranın y deki uzunluğunun yarısından az ise 

    private void SwipeDirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.up, direction) > directionThreshold) // not needed swipe up and swipe down right now 
        {
            Debug.Log("Swipe up");
        }
        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            Debug.Log("Swipe down");
        }
        else if (((Vector2.Dot(Vector2.left, direction) > directionThreshold) && (startPosition.y <= Screen.height/2f)) 
                 && !PlatformMovement.swipedLeft) // bug: it does not look whether our touch is below the half of the screen
        {
            PlatformMovement.swipedLeft = true;
            Debug.Log("Swipe left");
            StartCoroutine(platformGameObject.GetComponent<PlatformMovement>().getTurnPlatform());

        }
        else if (((Vector2.Dot(Vector2.right, direction) > directionThreshold) && (startPosition.y <= Screen.height/2f)) 
                 && !PlatformMovement.swipedRight)
        {
            PlatformMovement.swipedRight = true;
            Debug.Log("Swipe right");
            StartCoroutine(platformGameObject.GetComponent<PlatformMovement>().getTurnPlatform());
            
        }
    }
}
