using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Instantiator : MonoBehaviour
{
    public GameObject[] sths;
    private float a = 0,b=0;
    public int existNum = 0;
    public bool todo = true;

    private void Start()
    {
        var obj = sths[rand()];
        Instantiate(obj,rand(-20,15),obj.transform.rotation);
        existNum++;
    }

    void Update()
    {
        if (todo)
        {
            if (a > 6)//敌人生成时间固定
            {
                Instantiate(sths[2],rand(-20,15),sths[2].transform.rotation);
                a = 0;
            }
            if (existNum < 1&& b > 12)//当无buff块时，且时间大于12s，生成buff
            {
                var obj = sths[rand()];
                Instantiate(obj,rand(-20,15),obj.transform.rotation);
                b = 0;
                existNum++;
            }
            else if(existNum < 1)
            {
                b += Time.deltaTime;
            }
            a += Time.deltaTime;
        }
        
    }

    int rand()
    {
        int a = Random.Range(0, sths.Length-1);//确保生成buff
        return a;
    }

    Vector3 rand(float min, float max)
    {
        Vector3 a = new Vector3(Random.Range(min, max), 1, Random.Range(-max, 0));
        return a;
    }
}
