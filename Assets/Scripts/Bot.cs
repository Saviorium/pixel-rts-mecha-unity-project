using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Unit
{
    public GameObject ammo;
    public GameObject Explosion;

    protected override void InitComponents()
    {
        AddModule<BotBody>(); //TODO: make other modules as prefabs
        AddModule<Core>();
    }

    void OnDestroy()
    {
        GameObject explode = Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(explode,1);
    }
}
