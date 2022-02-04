using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Blocks : MonoBehaviour
{ 
    // this code contains the movement of block prefabs:
    // their falling,
    // collision and interaction,
    // their vertical movement with the touch input
    
    private Rigidbody2D rb;
    private GameObject platform;
    private SpawnArea spawnArea;
    private Touch touch;

    private bool StartedFixedUpdate;
    private GameObject lowestPositionObject;

    private bool startedFollowingTouch;
    
    private void Awake()
    {
        spawnArea = GameObject.Find("Spawn Area").GetComponent<SpawnArea>(); // instead of _spawnArea = new SpawnArea(); because it cannot be written like this
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartedFixedUpdate = true;
        platform = GameObject.Find("Platform");
        if (transform.GetChild(0).gameObject == null)
        {
            Debug.Log("Error : The prefab block does not have the child object (lowest position) . Create it");
        }else 
            lowestPositionObject = transform.GetChild(0).gameObject;
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
        StartCoroutine(Wait()); // start following the touch after some time
        Update();
    }

    private IEnumerator Wait() 
    {
        Debug.Log("started coroutine");
        yield return new WaitForSeconds(2.3f); // the waiting time to start following the touch
        // the time can be changed later
        Debug.Log("waited coroutine");
        startedFollowingTouch = true;
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
        if ((Input.GetMouseButton(0) || Input.touchCount > 0) && (GameManager.inSceneB && startedFollowingTouch))
        {
            Vector3 blockStopVector =
                new Vector3(transform.position.x, (Screen.height / 10) * 7, transform.position.z);

            if (lowestPositionObject.transform.position.y >= Camera.main.ScreenToWorldPoint(blockStopVector).y)
            {
                // tam bu kodda obje horizontal touch inputu takip ediyor 
                Debug.Log("şu an inputu takip eden koda girdi");
                
                Vector3 screenPos = Input.mousePosition;
                screenPos.z = 10.0f;
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
                Vector3 objPos = transform.position;
                objPos.x = worldPos.x;
                transform.position = objPos;
            }
        }
    }
}
