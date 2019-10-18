using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : Chassis
{
    void Start()
    {
        hp = 50;
        addons.Add(ScriptableObject.CreateInstance<Accuracy>());
        isCritical = true;
    }
}
