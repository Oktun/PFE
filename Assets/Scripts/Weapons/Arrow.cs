﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody rb;
    BoxCollider bx;
    bool disableRotation;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bx =transform.GetChild(0).GetComponent<BoxCollider>();
        //Destroy GamePbject
        Destroy(gameObject, 7f);
    }

    void Update()
    {
        if(!disableRotation)
            transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            disableRotation = true; 
            rb.isKinematic = true;
            bx.isTrigger = true;
        }
    }
}