using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject platform;
    private bool StartedFixedUpdate;
    
    private void Awake()
    {
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
        SpawnArea.StartedSpawn = true;
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
}