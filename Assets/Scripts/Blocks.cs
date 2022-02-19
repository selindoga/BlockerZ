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
    
    private Rigidbody2D _rb;
    private GameObject _platform;
    private SpawnArea _spawnArea;
    private Touch _touch;

    private bool _startedFixedUpdate;
    private GameObject _lowestPositionObject;

    private bool _startedFollowingTouch;
    
    private void Awake()
    {
        _spawnArea = GameObject.Find("Spawn Area").GetComponent<SpawnArea>(); // instead of _spawnArea = new SpawnArea(); because it cannot be written like this
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _startedFixedUpdate = true;
        _platform = GameObject.Find("Platform");
        if (transform.GetChild(0).gameObject == null)
        {
            Debug.Log("Error : The prefab block does not have the child object (lowest position) . Create it");
        }else 
            _lowestPositionObject = transform.GetChild(0).gameObject;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        // in the future:
        // check if other is the platform or its children that it should collide with,
        // if I happen to add more different objects near to the platform
        
        _startedFixedUpdate = false;
        gameObject.transform.SetParent(_platform.transform);
        _spawnArea.StartedSpawn = true;
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
        _startedFollowingTouch = true;
    }
    private void FixedUpdate()
    {
        if (_startedFixedUpdate)
        {
            _rb.MovePosition(_rb.position + new Vector2(0,-1) * Time.fixedDeltaTime);
        }
    }
    private void Update()
    {
        if ((Input.GetMouseButton(0) || Input.touchCount > 0) && (GameManager.inSceneB && _startedFollowingTouch))
        {
            Vector3 blockStopVector = new Vector3(transform.position.x, (Screen.height / 10) * 7, transform.position.z);
            if (_lowestPositionObject.transform.position.y >= Camera.main.ScreenToWorldPoint(blockStopVector).y)
            {
                // tam bu kodda obje horizontal touch inputu takip ediyor 
                Vector3 screenPos = Input.mousePosition;
                screenPos.z = 10.0f;
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
                Vector3 objPos = transform.position;
                objPos.x = worldPos.x;
                gameObject.transform.position = objPos;
            }
        }
    }
}
