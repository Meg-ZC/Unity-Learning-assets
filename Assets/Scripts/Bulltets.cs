using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Bulltets : MonoBehaviour
{
    private Rigidbody rb;
    public float force;

    private float alivetime = 0;
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        rb.AddForce(-transform.forward*force,ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y<-2||alivetime > 10)
            Destroy(gameObject);
        alivetime += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("enemy"))
        {
            // collision.rigidbody.useGravity = false;
            Destroy(collision.gameObject);
            
        }
    }
}
