using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPart : UnitModule
{
    public float speed;
    public float weightCarry;
    public bool  isFloating;

    public float GetSpeed(float overallweigth)
    {
        return (overallweigth / weightCarry) * speed;
    }

    protected override void DestroySelf() {
        isDestroyed = true;
        speed = 0;
    }
}
