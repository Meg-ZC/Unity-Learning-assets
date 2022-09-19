using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// A special building that hold a static reference so it can be found by other script easily (e.g. for Unit to go back
/// to it)
/// </summary>
public class Base : Building
{ 
    public static Base Instance { get; private set; }
    public Slider HPslider;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        HPslider.value = (float)((InventorySpace - m_CurrentAmount))/InventorySpace;
        // print(HPslider.value);
    }
}
