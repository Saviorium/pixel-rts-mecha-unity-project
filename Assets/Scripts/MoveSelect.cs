using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelect
{
    
    public GameObject target;

    public MoveSelect(GameObject targ)
    {
        target = targ;
        Instantiate(myPrefab, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(target.transform.position);
    }
}
