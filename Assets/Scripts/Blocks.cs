using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Blocks : MonoBehaviour
{ 
    // this code contains the movement of block prefabs: their falling, collision and interaction, their vertical movement with the touch input
    
    private Rigidbody2D rb;
    private GameObject platform;
    private SpawnArea spawnArea;
    private Touch touch;

    private bool StartedFixedUpdate;
    
    private void Awake()
    {
        spawnArea = GameObject.Find("Spawn Area").GetComponent<SpawnArea>(); // instead of _spawnArea = new SpawnArea(); because it cannot be written like this
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartedFixedUpdate = true;
        platform = GameObject.Find("Platform");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // in the future:
        // check if other is the platform or its children that it should collide with,
        // if I happen to add more different objects near to the platform
        
        StartedFixedUpdate = false;
        gameObject.transform.SetParent(platform.transform);
        spawnArea.StartedSpawn = true;
    }

    private void OnEnable()
    {
        FixedUpdate();
        Update();
    }

    private void FixedUpdate()
    {
        if (StartedFixedUpdate)
        {
            rb.MovePosition(rb.position + new Vector2(0,-1) * Time.fixedDeltaTime);
        }
    }

    private void Update()
    {
        if ((Input.GetMouseButton(0) || Input.touchCount > 0) && GameManager.inSceneB)
        {
            Vector3 screenPos = Input.mousePosition;
            screenPos.z = 10.0f;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            Vector3 objPos = transform.position;
            objPos.x = worldPos.x;
            transform.position = objPos;
        }
        
        // if (Input.touchCount > 0) // && Input.GetTouch(0).phase == TouchPhase.Began  I should remove the touchPhase part maybe
        // {
        //     touch = Input.GetTouch(0);
        //     
        //     Debug.Log("IF");
        //     
        //     if (GameManager.inSceneB && !CoroutineStarted)
        //     {
        //         //StartCoroutine(FollowTouch_B());
        //     }
        // }
        // else
        // {
        //     CoroutineStarted = false;
        //     StopCoroutine(FollowTouch_B());
        // }
    }

    // for design B that follows player's vertical touch position

    //private IEnumerator FollowTouch_B()
    //{
    //    while (true)
    //    {
    //        CoroutineStarted = true;
//
    //        Vector2 touchPosition = touch.position;
    //        Vector3 screenCoortinates = new Vector3(touchPosition.x, touchPosition.y, CameraMain.nearClipPlane);
    //        Vector3 worldCoordinates = CameraMain.ScreenToWorldPoint(screenCoortinates);
    //        worldCoordinates.y = 0;
    //        worldCoordinates.z = 0;
    //        transform.position = worldCoordinates;
//
    //        yield return null; 
    //    }
    //}
    
    
}
