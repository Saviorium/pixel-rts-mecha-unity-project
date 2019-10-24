using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

   void FixedUpdate ()
    {
        float Horizontal_x = Input.GetAxis("Horizontal");
        float Vertical_z = Input.GetAxis("Vertical");
        float Scroll = Input.GetAxis("Mouse ScrollWheel");
        
        GetComponent<Camera>().orthographicSize  += Scroll;
        gameObject.transform.position = new Vector3( gameObject.transform.position.x + Horizontal_x, 
                                                     gameObject.transform.position.y + Vertical_z ,
                                                     -10);
    }
}
