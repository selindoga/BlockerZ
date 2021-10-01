using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlatformMovement_B : MonoBehaviour
{
    // Code for design B

    private float turningDuration = 0.3f;
    
    private IEnumerator TurnPlatform()
    {
        Tween tween = transform.DORotate(new Vector3(0, 0, -90f), turningDuration, RotateMode.LocalAxisAdd).SetEase(Ease.Linear);
        yield return tween.WaitForCompletion();
    }

    public IEnumerator getTurnPlatform()
    {
        return TurnPlatform();
    }
}
