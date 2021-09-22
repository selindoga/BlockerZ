using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnArea : MonoBehaviour
{
    // kodu şimdilik sadece bu touch inputu kullanacakmışım gibi yazayım
    // tüm özellikleri kodlamayı bitirince touch inputu değiştir
    
    //      remove slide touch input
    //      instead add touch and hold input to change the position of the block vertically
    
    //          blokların rigidbodylerindeki freeze position y yi kaldır
    
    public GameObject [] ArrayOfBlocks;
    public static bool StartedSpawn;
    
    void Start()
    {
        StartedSpawn = true;
        StartCoroutine(MakeSpawn());
    }
    
    IEnumerator MakeSpawn()
    {
        while (true)
        {
            if (StartedSpawn)
            {
                int i = Random.Range(0, 2); // if written Random.Range for int the max value is exclusive
                Instantiate(ArrayOfBlocks[i], gameObject.transform.position, Quaternion.identity);
                StartedSpawn = false;
                yield return null;
            }
            else yield return null;
        }
    }
}
