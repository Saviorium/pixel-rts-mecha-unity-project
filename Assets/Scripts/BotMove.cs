using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMove : MonoBehaviour
{
    public float head;
    public float body;
    public float legs;

    public string name;
    public bool isChoosed;

    public float moveSpeed = 20f;
    private Vector3 moveTarget = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        head = 100;
        body = 100;
        legs = 100;

        name = "Bot-4000";
        isChoosed = false;
    }

    void OnMouseDown ()
    {
        isChoosed = !isChoosed;
        Debug.Log(isChoosed);
    }

    void Update()
    {
        if ((Input.GetMouseButtonDown(1)) && (isChoosed)) {
            moveTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moveTarget.Scale(new Vector3(1f, 1f, 0f));
        }

        Vector3 travelVector = moveTarget - transform.position;
        if(travelVector.magnitude > 0.1f) {            
            transform.Translate(travelVector.normalized * moveSpeed * Time.deltaTime);
        }
    }
}
