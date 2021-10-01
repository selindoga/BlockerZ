using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{ 
    // this code contains the movement of block prefabs: their falling, collision and interaction, their horizontal movement with the touch input
    
    private Camera CameraMain;
    private Rigidbody2D rb;
    private GameObject platform;
    private bool StartedFixedUpdate;
    private SpawnArea spawnArea;
    private Touch touch;
    
    private void Awake()
    {
        CameraMain = Camera.main;
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
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touch = Input.GetTouch(0);
            Debug.Log("inside the change block UPDATE");
            // MoveBlock_B(touch.position);
        }
    }

    // also write the code for design B that follows player's horizontal touch position
    private void MoveBlock_B(Vector2 screenPosition) 
        // have no idea how to combine this with the touch input 
        // need to look a tutorial bout it
    {
        Vector3 screenCoortinates = new Vector3(screenPosition.x, screenPosition.y, CameraMain.nearClipPlane);
        Vector3 worldCoordinates = CameraMain.ScreenToWorldPoint(screenCoortinates);
        worldCoordinates.y = 0;
        // worldCoordinates.z = 0;
        transform.position = worldCoordinates;
    }
    
    
}
