using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    // this script is in blocks' prefabs.
    
    private GameObject CameraFollowingParent;
    private CameraMovement _cameraMovement;
    private void Awake()
    {
        CameraFollowingParent = GameObject.Find("CameraFollowingParent");
        _cameraMovement = CameraFollowingParent.GetComponent<CameraMovement>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("forCameraMov")) // todo : şu an bu kodda ne oldu
            // todo: tam burada platforma düşen block ekranın kenarlarındaki colliderlara çarpıyor mu ?
        {
            _cameraMovement.ChangeCameraMovement();
        }
    }
}
