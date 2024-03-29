using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    // this is for Camera Movement
    // this script is in blocks' prefabs. Block parent object
    
    private GameObject CameraFollowingParent;
    private CameraMovement _cameraMovement;
    private void Awake()
    {
        CameraFollowingParent = GameObject.Find("CameraFollowingParent");
        _cameraMovement = CameraFollowingParent.GetComponent<CameraMovement>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("forCameraMov"))
        {
            _cameraMovement.ChangeCameraMovement();
        }
    }
}
