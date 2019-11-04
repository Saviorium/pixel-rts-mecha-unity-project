using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Unit
{
    public GameObject ammo;
    public GameObject Explousion;

    protected override void InitComponents()
    {
        AddModule<BotBody>();
        AddModule<BotLegs>();
        AddModule<Core>();
        AddModule<Gatling>();
    }

    void OnDestroy()
    {
        GameObject exploud = Instantiate(Explousion, transform.position, Quaternion.identity);
        Destroy(exploud,1);
    }
}
