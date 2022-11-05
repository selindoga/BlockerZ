using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // this script is in CameraFollowingParent object
    public static bool Scaled;
    private SpawnArea _spawnArea;
    private Camera _mainCamera;
    private float _cameraAddScaleValue = 0.05f;
    private GameObject _platform;
    private float _cameraCurrentScaleValue;

    private void Awake()
    {
        _spawnArea = GameObject.Find("Spawn Area").GetComponent<SpawnArea>();
        _mainCamera = Camera.main;
        _platform = GameObject.Find("Platform");
    }

    public void ChangeCameraMovement()
    {
        if (_spawnArea.BlockPlacedToPlatform_StartedSpawningBlock && !Scaled)
        {
            Vector3 vector3 = new Vector3(_cameraAddScaleValue, _cameraAddScaleValue, 0);
            gameObject.transform.localScale += vector3;
            
            // the code below changes the camera's size (scale)
            _mainCamera.orthographicSize = (gameObject.transform.localScale.x * 5f);
            _cameraCurrentScaleValue = gameObject.transform.localScale.x;
            Scaled = true;
            ChangeBlocksScale(_platform,"platform");
        }
    }
    
    //              all blocks which were placed to the platform must have the tag "platform"
    //              Any other GameObjects must not have this tag
    private void ChangeBlocksScale(GameObject parent, String tag)
    {
        // this is the scale that all the block who are on the platform must have.
        Vector3 vector3 = new Vector3(1/_cameraCurrentScaleValue, 1/_cameraCurrentScaleValue, 1);
        
        List<GameObject> blockObjects = new List<GameObject>();
        blockObjects.Add(parent.GetComponentsInChildren<Transform>().FirstOrDefault(r => r.CompareTag(tag)).gameObject);
        foreach(var blocks in blockObjects)
        {
            blocks.gameObject.transform.localScale = vector3;
        }
        //foreach (Transform children in rootObjectOfEnemy.transform.GetComponentsInChildren<Transform>())
        //{
        //    children.gameObject.layer = XrayLayerValue;
        //}
    }

}
