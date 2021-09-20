using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    public GameObject A2;
    private bool StartedSpawn = true;
    
    
    void Start()   
    {
        StartCoroutine(MakeSpawn());
    }
    
    IEnumerator MakeSpawn()
    {
        if (StartedSpawn)
        {
            Instantiate(A2, gameObject.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(3f);
            StartedSpawn = false;
        }
        else yield return new WaitForSeconds(3f);
    }
}
