using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Addon : MonoBehaviour
{
    protected List<int> Bufs = new List<int> {0 /*Дальность обзора*/
                                   ,0/*Дальность стрельбы */
                                   ,0/*Урон */
                                   ,0/*Точность */
                                   ,0/*Скорость */
                                   ,0/*Увороты */}; // все бафы в процентах

    public List<int> GetBufs()
    {
        return Bufs;
    }
}
