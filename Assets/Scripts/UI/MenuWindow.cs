using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuWindow : Window
{ 
    protected override void SelfOpen()
    {
        gameObject.SetActive(true);
    }

    protected override void SelfClose()
    {
        gameObject.SetActive(false);
    }

}
