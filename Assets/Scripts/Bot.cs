using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Unit
{
    public GameObject ammo;

    protected override void InitComponents()
    {
        AddModule<BotBody>();
        AddModule<BotLegs>();
        AddModule<Core>();
        AddModule<Gatling>();
    }
}
