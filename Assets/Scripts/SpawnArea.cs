using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnArea : MonoBehaviour
{
    // blokların rigidbodylerindeki freeze position y yi kaldır
    
    public GameObject [] ArrayOfBlocks;
    private bool startedSpawn;
    
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
                int i = Random.Range(0, 2); // if written Random.Range for int the max value is exclusive
                Instantiate(ArrayOfBlocks[i], gameObject.transform.position, Quaternion.identity);
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
