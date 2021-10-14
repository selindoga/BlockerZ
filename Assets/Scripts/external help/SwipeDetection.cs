using System;
using System.Collections;
using UnityEngine;

// This code was copied from Samyam's video tutorial
// The code was changed, removed and new codes were added by me

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
    
    private void Awake()
    {
        inputManager = InputManager.Instance;
        platformGameObject = GameObject.Find("Platform");
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
        if (GameManager.inSceneA)
        {
            if (Vector3.Distance(startPosition, endPosition) >= minimumDistance && (endTime - startTime) <= maximumTime)
            {
                Debug.DrawLine(startPosition, endPosition, Color.red, 5f); 
                Vector3 direction = endPosition - startPosition;
                Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
                SwipeDirection(direction2D);
            }
        }
    } 

    private void SwipeDirection(Vector2 direction)
    {
        /*
        if (Vector2.Dot(Vector2.up, direction) > directionThreshold) // not needed swipe up and swipe down right now ( NOT YET )
        {
            Debug.Log("Swipe up");
        }
        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            Debug.Log("Swipe down");
        }
        */
        
        if (((Vector2.Dot(Vector2.left, direction) > directionThreshold) && (Input.GetTouch(0).position.y <= (Screen.height / 2f))) 
            && !PlatformMovement_A.swipedLeft)
            // Check for design A: Does it look whether our touch position is below the half of the screen?
        {
            if (GameManager.inSceneA) 
                PlatformMovement_A.swipedLeft = true;
            
            Debug.Log("Swipe left");
            StartCoroutine(platformGameObject.GetComponent<PlatformMovement_A>().getTurnPlatform());
            
        }
        else if (((Vector2.Dot(Vector2.right, direction) > directionThreshold) && (Input.GetTouch(0).position.y <= (Screen.height/2f))) 
                 && !PlatformMovement_A.swipedRight)
        {
            if (GameManager.inSceneA) 
                PlatformMovement_A.swipedRight = true;
            
            Debug.Log("Swipe right");
            StartCoroutine(platformGameObject.GetComponent<PlatformMovement_A>().getTurnPlatform());
            
        }
    }
}
