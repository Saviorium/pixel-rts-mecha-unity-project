using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotWings : MovingPart
{
    void Start()
    {
        hp = 20;
        speed = 300f;
        weight = 20f;
        weightCarry = 200f;
        isFloating = true;
    }
}
