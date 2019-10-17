using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chassis : MonoBehaviour
{
    protected float hp;
    protected float weigth;

    public void TakeDamage(float damage)
    {
        hp = hp - damage;
    }

    public float GetHp()
    {
        return hp;
    }
    
    public float GetWeigth()
    {
        return weigth;
    }
}
