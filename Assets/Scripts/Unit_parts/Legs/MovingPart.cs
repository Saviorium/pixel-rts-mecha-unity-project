using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPart : MonoBehaviour
{
    protected float hp;
    protected float speed;
    protected float weigth;
    protected float weigth_carry;
    protected bool  IsDestroyed;
    protected bool  IsFloating;

    protected void Start()
    {
        IsDestroyed = false;
    }

    public float GetSpeed(float overallweigth)
    {
        return speed - (overallweigth / weigth_carry * 100 ) * speed;
    }

    public bool IsFloat()
    {
        return IsFloating;
    }

    public void TakeDamage(float damage)
    {
        hp = hp - damage;
        IsDestroyed = true;
        speed = 0;
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
