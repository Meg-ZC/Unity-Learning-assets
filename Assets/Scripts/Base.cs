using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// A special building that hold a static reference so it can be found by other script easily (e.g. for Unit to go back
/// to it)
/// </summary>
public class Base : Building
{ 
    public static Base Instance { get; private set; }
    public Slider HPslider;
    public UserControl UserControl;
    private bool todo = true;
    public Button exit;
    public Instantiator Instantiator;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        HPslider.value = (float)((InventorySpace - m_CurrentAmount))/InventorySpace;
        if (m_CurrentAmount >= 95&&todo)
        {
            todo = false;
            UserControl.alive = false;
            exit.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Instantiator.todo = false;
        }
        // print(HPslider.value);
    }

    public void backToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
