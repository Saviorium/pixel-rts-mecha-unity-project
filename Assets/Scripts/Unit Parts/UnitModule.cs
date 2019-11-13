using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitModule : MonoBehaviour
{
    public float hp = 100f;
    public float weight = 10;
    protected bool isDestroyed = false;
    public bool isCritical = false;
    public List<Addon> addons = new List<Addon>();
    public List<Task.TaskType> AvailableTasks = new List<Task.TaskType>();

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0) {
            DestroySelf();
            if (isCritical) {
                KillUnit();
            }
        }
    }

    protected virtual void DestroySelf()
    {
        isDestroyed = true;
    }

    private void KillUnit() {
        gameObject.GetComponentInParent<Unit>().Die();
    }
}
