using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot_Legs_1 : MovingPart
{
    // Start is called before the first frame update
    void Start()
    { 
        base.Start();
        hp = 100;
        speed = 100f;
        weigth = 50f;
        weigth_carry = 300f;
        IsFloating = false;
        Debug.Log("legs");
    }

}
