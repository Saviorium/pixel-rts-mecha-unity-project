using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_System : MonoBehaviour
{
    protected float hp;
    protected bool IsDestroyed;
    protected List<Addon> Addons; 

    public List<int> CountBufs()
    {
        if (!IsDestroyed)
        {
            List<int> Bufs = new List<int> {0 /*Дальность обзора*/
                                           ,0/*Дальность стрельбы */
                                           ,0/*Урон */
                                           ,0/*Точность */
                                           ,0/*Скорость */
                                           ,0/*Увороты */}; // все бафы в процентах
            foreach (Addon add in Addons)
            {
                List<int> AddBufs = add.GetBufs();
                for (int i = 0; i < Bufs.Count; i++)
                    Bufs[i] += AddBufs[i];
            }
            return Bufs;
        }
        return null;
    }

    public void TakeDamage(float damage)
    {
        hp = hp - damage;
        IsDestroyed = true;
    }
    
    public float GetHp()
    {
        return hp;
    }
}
