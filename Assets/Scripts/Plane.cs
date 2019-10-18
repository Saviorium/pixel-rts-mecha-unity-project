using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : Unit
{
    protected override void InitComponents()
    {
        AddModule<BotBody>();
        AddModule<BotWings>();
        AddModule<Core>();
        AddModule<Gatling>();
    }
}
