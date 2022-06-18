using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // blok platforma yerleştikten sonra bakacağım: 
    // blok screen wider colliderlara çarpıyor mu 
    //   çarpıyorsa onun parent ı olan CameraFollowingParent ın x ve y deki scale ini arttır.
                               
    // this script is in CameraFollowingParent object
    
    private SpawnArea _spawnArea;
    private Camera _mainCamera;
    private float _cameraScaleValue = 0.2f;

    private void Awake()
    {
        _spawnArea = GameObject.Find("Spawn Area").GetComponent<SpawnArea>();
        _mainCamera = Camera.main;
    }

    public void ChangeCameraMovement()
    {
        if (!_spawnArea.StartedSpawingBlocks) // todo buralarda bir hata olabilir
        // neden blokların aniden çokça spawnlandığını ve platformun sürekli döndüğünü bul
        // nedeni buradan kaynaklanıyor olabilir
        {
            Vector3 _vector3 = new Vector3(_cameraScaleValue, _cameraScaleValue, 0);
            gameObject.transform.localScale += _vector3;
            
            // the code below changes the camera's size (scale)
            _mainCamera.orthographicSize = (gameObject.transform.localScale.x * 5f);
        }
    }

}
