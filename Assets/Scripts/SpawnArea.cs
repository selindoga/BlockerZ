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
    private bool startedSpawn;
    private PlatformMovement_B _platformMovementB;
    void Start()
    {
        startedSpawn = true;
        StartCoroutine(MakeSpawn());
    }
    
    IEnumerator MakeSpawn()
    {
        while (true)
        {
            if (startedSpawn)
            {
                if (GameManager.inSceneB)
                {
                    _platformMovementB = GameObject.Find("Platform").GetComponent<PlatformMovement_B>();
                    StartCoroutine(_platformMovementB.getTurnPlatform());
                }
                yield return new WaitForSeconds(.4f); // platform turning must be at most 0.5 seconds
                int i = Random.Range(0, 2); // if written Random.Range for int the max value is exclusive
                Instantiate(ArrayOfBlocks[i], transform.position, Quaternion.identity);
                Debug.Log("just spawned other block");
                startedSpawn = false;
                yield return null;
            }
            else yield return null;
        }
    }

    public bool StartedSpawn // can be written with either => or { }
    {
        get => startedSpawn;
        set => startedSpawn = value;
    }
}
