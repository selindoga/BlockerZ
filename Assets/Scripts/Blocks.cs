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
    private SpawnArea _spawnArea;
    private Touch _touch;

    private bool _startedFixedUpdate;
    private GameObject _lowestPositionObject;

    private bool _startedFollowingTouch;
    private Vector3 _blockStopVector;
    private Vector3 _screenPos;
    private Vector3 _worldPos;
    private Vector3 _objPos;
    
    private void Awake()
    {
        // instead of _spawnArea = new SpawnArea(); because it cannot be written like this
        _spawnArea = GameObject.Find("Spawn Area").GetComponent<SpawnArea>();
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _startedFixedUpdate = true;
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
        // in the future todo:
        // check if other is the platform or its children that it should collide with,
        // if I happen to add more different objects near to the platform
        
        if (other.gameObject.CompareTag("platform"))
        {
            _startedFixedUpdate = false;
            gameObject.transform.SetParent(_platform.transform);
            _spawnArea.BlockPlacedToPlatform_StartedSpawningBlock = true;
            tag = "platform";
            _startedFollowingTouch = false;
            CameraMovement.Scaled = false;
        }
    }
    private void OnEnable()
    {
        FixedUpdate();
        StartCoroutine(Wait()); // start following the touch after some time
        Update();
    }
    private IEnumerator Wait() 
    {
        yield return new WaitForSeconds(.3f); // the waiting time to start following the touch
        // the time can be changed later
        _startedFollowingTouch = true;
    }
    private void FixedUpdate()
    {
        if (_startedFixedUpdate)
        {
            _rb.MovePosition(_rb.position + new Vector2(0,-1) * Time.fixedDeltaTime);
        }
    }
    // just for the sake of the code quality todo: I have to change this Update() function below to a coroutine instead.
    private void Update()
    {
        if ((Input.GetMouseButton(0) || Input.touchCount > 0) && (GameManager.inSceneB && _startedFollowingTouch))
        {
            FollowTouchInput();
        }
    }

    private void FollowTouchInput()
    {
        _blockStopVector = new Vector3(transform.position.x, (Screen.height / 10) * 7, transform.position.z);
        if (_lowestPositionObject.transform.position.y >= Camera.main.ScreenToWorldPoint(_blockStopVector).y)
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

    public float GetSnappingValue(float number)
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
