using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatling : Gun
{
    void Start()
    {
        damageToObjects = 1;
        damageToUnits = 10;
        fireRate = 5;
        range = 8;
        accuracy = .15f;
        ammo = (GameObject)Resources.Load("Bullet Fire");

        isAgainstSurface = true;
        isAgainstAir = true;
    }
}
