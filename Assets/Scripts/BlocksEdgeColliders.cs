using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksEdgeColliders : MonoBehaviour
{
    public BoxCollider2D boxCollider2D_Vertical; // DO NOT RENAME THEM
    public BoxCollider2D boxCollider2D_Horizontal; // DO NOT RENAME THEM
    
    //private bool m_BlockPlaced; 
    //bool m_InitializesFirstActivity;
    //public static bool ChangesCollidersActivity;
    public static bool HorizontalCollidersActive;
    public static bool VerticalCollidersActive;
    private bool oneBool;
    
    private SpawnArea _spawnArea;
    private void Update()
    {
        //if (transform.parent.gameObject.CompareTag("platform") && !m_BlockPlaced)
        //{
        //    ChangeCollidersActivity();
        //    m_BlockPlaced = true;
        //}
        
        //if (ChangesCollidersActivity)
        //{
        //    ChangeCollidersActivity();
        //}
        
        if (transform.parent.gameObject.CompareTag("platform"))
        {
            //if(boxCollider2D_Vertical != null) boxCollider2D_Vertical.enabled = true;
            //if(boxCollider2D_Horizontal != null) boxCollider2D_Horizontal.enabled = false;
            //if (oneBool)
            //{
            //    if(boxCollider2D_Vertical != null) boxCollider2D_Vertical.enabled = true;
            //    if(boxCollider2D_Horizontal != null) boxCollider2D_Horizontal.enabled = false;
            //    oneBool = false;
            //}
            ChangeCollidersActivity();
            return;
        }
        else
        {
            if(boxCollider2D_Horizontal != null) boxCollider2D_Horizontal.enabled = HorizontalCollidersActive;
            if(boxCollider2D_Vertical != null) boxCollider2D_Vertical.enabled = !HorizontalCollidersActive;
        }
    }

    
    private void Start()
    {
        //m_InitializesFirstActivity = true;
        //if (!transform.parent.gameObject.CompareTag("platform"))
        //{
        //    ChangeCollidersActivity();
        //}
        Debug.Log("BEN BİR OBJEYE ATTACHLENDİM");
        Debug.Log(gameObject);
        oneBool = true;
        _spawnArea = GameObject.Find("Spawn Area").GetComponent<SpawnArea>();
    }

    public void ChangeCollidersActivity()
    {
        //if (m_InitializesFirstActivity)
        //{
        //    if(boxCollider2D_Vertical != null) boxCollider2D_Vertical.enabled = true;
        //    if(boxCollider2D_Horizontal != null) boxCollider2D_Horizontal.enabled = false;
        //    m_InitializesFirstActivity = false;
        //}
        //else
        //{
        //    if(boxCollider2D_Horizontal != null) boxCollider2D_Horizontal.enabled = !HorizontalCollidersActive;
        //    if(boxCollider2D_Vertical != null) boxCollider2D_Vertical.enabled = HorizontalCollidersActive;
        //}
        
        bool aaa = _spawnArea.BlockPlacedToPlatform_StartedSpawningBlock;
        
        if(boxCollider2D_Vertical != null) boxCollider2D_Vertical.enabled = aaa;
        if(boxCollider2D_Horizontal != null) boxCollider2D_Horizontal.enabled = !aaa;

    }
}
