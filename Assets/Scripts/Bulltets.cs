using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Bulltets : MonoBehaviour
{
    private Rigidbody rb;
    public float force;
    public Instantiator Instantiator;
    void Start()
    {
        Instantiator = GameObject.Find("Instantiator").GetComponent<Instantiator>();
        rb = GetComponent<Rigidbody>();
        rb.AddForce(-transform.forward*force,ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y<-2)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("enemy"))
        {
            // collision.rigidbody.useGravity = false;
            Destroy(collision.gameObject);
            Instantiator.existNum--;
        }
    }
}
