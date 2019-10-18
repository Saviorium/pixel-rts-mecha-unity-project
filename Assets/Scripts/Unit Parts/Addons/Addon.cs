using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Addon : ScriptableObject
{
    protected Dictionary<string, float> buffs = new Dictionary<string, float>(); //TODO: AddonType - enum

    void Start() {
        buffs.Add("radar", 0);
        buffs.Add("firerange", 0);
        buffs.Add("damage", 0);
        buffs.Add("accuracy", 0);
        buffs.Add("movespeed", 0);
        buffs.Add("maneurability", 0);
    }
    
    float applyBuff(string buffName, float property) {
        return buffs.ContainsKey(buffName) ? property + property * buffs[buffName] : property; //TODO: applyType: add, multiply, etc.
    }
}
