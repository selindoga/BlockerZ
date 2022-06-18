using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnArea : MonoBehaviour
{
    // This script can be used for both design A and B
    // reminder: remove freeze position y under the rigidbody component of the blocks
    
    // this script contains: the spawning of blocks, and calling the movement of platform
    public GameObject [] ArrayOfBlocks;
    private bool _startedSpawingBlocks;
    private PlatformMovement_B _platformMovementB;
    void Start()
    {
        _startedSpawingBlocks = true;
        StartCoroutine(MakeSpawn());
    }
    
    IEnumerator MakeSpawn()
    {
        while (true)
        {
            if (_startedSpawingBlocks)
            {
                if (GameManager.inSceneB)
                {
                    _platformMovementB = GameObject.Find("Platform").GetComponent<PlatformMovement_B>();
                    StartCoroutine(_platformMovementB.getTurnPlatform());
                }
                yield return new WaitForSeconds(.4f); // platform turning must be at most 0.5 seconds
                int i = Random.Range(0, 2); // if written Random.Range for int the max value is exclusive
                Instantiate(ArrayOfBlocks[i], transform.position, Quaternion.identity);
                _startedSpawingBlocks = false; 
                    // _startedSpawingBlocks becomes false right after the spawning of a block
                yield return null;
            }
            else yield return null;
        }
    }

    public bool StartedSpawingBlocks // can be written with either => or { }
    {
        get => _startedSpawingBlocks;
        set => _startedSpawingBlocks = value;
    }
}
