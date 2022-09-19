using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class Instantiator : MonoBehaviour
{
    public GameObject[] sths;
    private float a = 0;
    public int existNum = 0;
    void Update()
    {
        
        if (a > 3&& existNum < 20)
        {
            var obj = sths[rand()];
            Instantiate(obj,rand(-20,15),obj.transform.rotation);
            a = 0;
            existNum++;
        }

        a += Time.deltaTime;
    }

    int rand()
    {
        int a = Random.Range(0, sths.Length);
        return a;
    }

    Vector3 rand(float min, float max)
    {
        Vector3 a = new Vector3(Random.Range(min, max), 1, Random.Range(-max, 0));
        return a;
    }
}
