using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    public float moveSpeed = 20f;

    private Vector3 moveTarget = Vector3.zero;
    
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            moveTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moveTarget.Scale(new Vector3(1f, 1f, 0f));
        }

        Vector3 travelVector = moveTarget - transform.position;
        if(travelVector.magnitude > 0.1f) {            
            transform.Translate(travelVector.normalized * moveSpeed * Time.deltaTime);
        }
    }
}
