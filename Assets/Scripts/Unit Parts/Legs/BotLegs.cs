using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotLegs : MovingPart
{
    void Start()
    {
        hp = 100;
        speed = 100f;
        weight = 50f;
        weightCarry = 300f;
        isFloating = false;
    }
}
