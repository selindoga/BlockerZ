using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Blocks : MonoBehaviour
{ 
    // TODO: seperate this code and make it more readable (when i have some spare time :D)
    
    // this code contains the movement of block prefabs:
    // their falling,
    // collision and interaction,
    // their vertical movement with the touch input

    private Rigidbody2D _rb;
    private GameObject _platform;
    private GameObject _lowestPositionObject;
    private SpawnArea _spawnArea;

    private Touch _touch;
    private Vector3 _blockStopVector;
    private Vector3 _screenPos;
    private Vector3 _worldPos;
    private Vector3 _objPos;
    
    private bool _isFollowingTouchPosition;
    private bool _isFalling;
    private bool _inTouchableArea;

    private void Awake()
    {
        // instead of _spawnArea = new SpawnArea(); because it cannot be written like this
        _spawnArea = GameObject.Find("Spawn Area").GetComponent<SpawnArea>();
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _isFalling = true;
        _platform = GameObject.Find("Platform");
        try
        {
            _lowestPositionObject = transform.GetChild(0).gameObject;
        }
        catch
        {
            Debug.Log("Error : The prefab block does not have the child object (lowest position) . Create it");
            // it means transform.GetChild(0).gameObject == null
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!gameObject.CompareTag("platform") && other.gameObject.CompareTag("platform"))
        {
            gameObject.transform.SetParent(_platform.transform);
            _spawnArea.BlockPlacedToPlatform_StartedSpawningBlock = true;
            tag = "platform";
            CameraMovement.Scaled = false;
            _isFalling = false; 
            // block un child olduğu kısmı incele 
            // bu _isFalling false olduktan sonra child olmalı 
            _isFollowingTouchPosition = false;
        }
    }

    private void OnEnable()
    {
        FixedUpdate();
        StartCoroutine(Wait()); // start following the touch after some time
        //Update();
    }
    private IEnumerator Wait() 
    {
        yield return new WaitForSeconds(.3f); // the waiting time to start following the touch
        // the time can be changed later
        _isFollowingTouchPosition = true;
    }
    private void FixedUpdate()
    {
        if (_isFalling)
        {
            _rb.MovePosition(_rb.position + new Vector2(0,-1) * Time.fixedDeltaTime);
        }
    }
    // just for the sake of the code quality todo: I have to change this Update() function below to a coroutine instead.
    private void Update()
    {
        Vector3 pos = transform.position;
        _blockStopVector = new Vector3( pos.x, (Screen.height / 10) * 7, pos.z);
        _inTouchableArea = _lowestPositionObject.transform.position.y >=
                                    Camera.main.ScreenToWorldPoint(_blockStopVector).y;
        // Follow Touch Input
        if ((Input.GetMouseButton(0) || Input.touchCount > 0) &&
            (GameManager.inSceneB && _isFollowingTouchPosition) && _inTouchableArea)
        {
            ChangeObjectsPosition();
        }
    }

    private void ChangeObjectsPosition()
    {
        // tam bu kodda obje horizontal touch inputu takip ediyor 
        _screenPos = Input.mousePosition;
        _screenPos.z = 10.0f;
        _worldPos = Camera.main.ScreenToWorldPoint(_screenPos);
        _objPos = transform.position;
        _objPos.x = _worldPos.x;
        _objPos.x = GetSnappingValue(_objPos.x);
        gameObject.transform.position = _objPos;
    }

    private float GetSnappingValue(float number)
    {
        float snappingFractional = 0;
        float floored = Mathf.Floor(number);
        float fractionalPart = number - floored;

        if (fractionalPart >= 0 && fractionalPart <= 0.25)
        {
            snappingFractional = 0;
        }
        else if(fractionalPart > .25 && fractionalPart < .75)
        {
            snappingFractional = 0.5f;
        }
        else if (fractionalPart >= .75)
        {
            snappingFractional = 1;
        }
        
        return floored + snappingFractional;
    }
}
