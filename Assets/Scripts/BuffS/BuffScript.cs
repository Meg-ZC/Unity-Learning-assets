 using System;
 using System.Collections;
using System.Collections.Generic;
 using UnityEditor.PackageManager;
 using UnityEngine;
 using Random = UnityEngine.Random;

 public class BuffScript : MonoBehaviour
{
    private float height = 0.5f;
    private Rigidbody objec;
    public UserControl obj;
    public float existTime = 0;
    public Instantiator Instantiator;
    private void Awake()
    {
        objec = GetComponent<Rigidbody>();
        obj = GameObject.Find("UserControl").GetComponent<UserControl>();
        Instantiator = GameObject.Find("Instantiator").GetComponent<Instantiator>();
    }

    void Update()
    {
        existTime += Time.deltaTime;
        Ray ray= new Ray(transform.position, Vector3.down);
        // Debug.DrawRay(transform.position,Vector3.down*height);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, height)) 
        {
            if (hitInfo.transform.CompareTag("ground"))
            {
                objec.AddForce(Vector3.up*objec.mass*20);
            }
        }

        if (transform.position.y < -2 || existTime > 8)
        {
            Instantiator.existNum--;
            Destroy(gameObject);
        }
        //
        // transform.rotation = Quaternion.Euler(randDir);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

                    obj.ChangeSpeed();
                    Debug.Log("speed up!");
                    Instantiator.existNum--;
                    Destroy(transform.gameObject);
        }
    }
}
