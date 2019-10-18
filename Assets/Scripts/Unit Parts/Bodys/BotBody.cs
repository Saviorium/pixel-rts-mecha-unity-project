using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBody : Chassis
{
    void Start()
    {
        hp = 100f;
        weight = 200f;
        isCritical = true;
    }
}
