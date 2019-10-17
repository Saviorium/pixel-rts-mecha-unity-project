using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accuraty : Addon
{
    // Start is called before the first frame update
    void Start()
    {
        List<int> Bufs = new List<int> {0 /*Дальность обзора*/
                                    ,0/*Дальность стрельбы */
                                    ,0/*Урон */
                                    ,50/*Точность */
                                    ,0/*Скорость */
                                    ,0/*Увороты */}; // все бафы в процентах
    }
}
