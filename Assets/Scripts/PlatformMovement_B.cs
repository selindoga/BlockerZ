using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlatformMovement_B : MonoBehaviour
{
    // Code for design B
    private static bool lineColliderIsHorizontal = true;
    private float turningDuration = 0.3f;
    private GameObject lineCollider;
    private GameObject lineColliderOther;
    
    private void Awake()
    {
        lineCollider = GameObject.Find("Platform/lineCollider");
        lineColliderOther = GameObject.Find("Platform/lineColliderOther");
    }
    private IEnumerator TurnPlatform()
    {
        //Debug.Log("in the ienumerator");
        Tween tween = transform.DORotate(new Vector3(0, 0, -90f), turningDuration, RotateMode.LocalAxisAdd).SetEase(Ease.Linear);
        yield return tween.WaitForCompletion();
        lineColliderIsHorizontal = !lineColliderIsHorizontal;
    }
    public IEnumerator getTurnPlatform()
    {
        return TurnPlatform();
    }
    private void Update()
    {
        lineCollider.SetActive(lineColliderIsHorizontal);
        lineColliderOther.SetActive(!lineColliderIsHorizontal);
    }
}
