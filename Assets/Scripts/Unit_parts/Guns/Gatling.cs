using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatling : Gun
{
    public GameObject Ammo;

    void Start()
    {
        damage_to_objects = 1;
        damage_to_units = 10;
        rate_of_fire = 5;
        range = 3;
        this.Ammo = Ammo;

        IsAgainstSurface = true;
        IsAgainstAir = true;
    }

}
