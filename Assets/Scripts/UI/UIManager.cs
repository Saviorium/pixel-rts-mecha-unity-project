using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;

    public List<GameObject> Windows;

    void Awake()
    {
    if (Instance == null)
        Instance = this;
    else if (Instance != this)
        Destroy(gameObject);

    //  InitUI();
    }

    // private void InitUI()
    // {
    //     //to do
    // }

    void Start()
    {
        foreach(var window in Windows)
        {
            var windowComponent = window.GetComponent<Window>();
            // if (windowComponent is StartWindow)
            //     windowComponent.Open();
            // else
                windowComponent.Close();
        }
    }

    public Window Get<T> () where T : Window
    {
        foreach(var window in Windows)
        {
            var windowComponent = window.GetComponent<Window>();
            if (windowComponent is T)
                return windowComponent;
        }
        return null;
    }
}