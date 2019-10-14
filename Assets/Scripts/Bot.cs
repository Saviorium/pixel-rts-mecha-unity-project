using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Unit
{
    public float head = 100;
    public float body = 100;
    public float legs = 100;

    public override void TakeDamage(float damage) {
        switch (Random.Range(0,3)){
            case 0:
            {
                head -= damage; break;
            }
            case 1:
            {
                body -= damage; break;
            }
            case 2:
            {
                legs -= damage; break;
            }
        }
        if(body < 0f || head < 0f) {
            Die();
        }
    }
}
