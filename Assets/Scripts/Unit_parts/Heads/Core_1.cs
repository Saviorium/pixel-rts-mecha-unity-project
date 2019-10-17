using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core_1 : Unit_System
{
    // Start is called before the first frame update
    void Start()
    {
        hp = 100;
        IsDestroyed = false;
        Addons = new List<Addon> {new Accuraty()};
    }

}
