using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public GameObject MenuWindow;
    
    void Start()
    {
		gameObject.GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        if (MenuWindow.GetComponent<Window>().IsOpen)
            MenuWindow.GetComponent<Window>().Close();
        else
            MenuWindow.GetComponent<Window>().Open();
    }

}
