using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockColliders : MonoBehaviour
{
    private BoxCollider2D _boxCollider2D;
    
    private float wide = 0.5f;
    private float edradiusZ = 0;

    private float notWide = 0.3f;
    private float edradius = 0.1f;

    private PhysicsMaterial2D _physicsMaterial2D;
    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _physicsMaterial2D = new PhysicsMaterial2D("material");
        
        _physicsMaterial2D.bounciness = 0;
        _physicsMaterial2D.friction = 1f;
    }

    private void Start()
    {
        _boxCollider2D.sharedMaterial = _physicsMaterial2D;
        addPhyMat_bigSize();
    }

    private void addPhyMat_smallSize()
    {
        _boxCollider2D.size = new Vector2(notWide, notWide);
        _boxCollider2D.edgeRadius = edradius;
    }

    private void addPhyMat_bigSize()
    {
        _boxCollider2D.size = new Vector2(wide, wide);
        _boxCollider2D.edgeRadius = edradiusZ;
    }
}
