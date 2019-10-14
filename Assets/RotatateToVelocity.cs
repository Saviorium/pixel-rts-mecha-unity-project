using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatateToVelocity : MonoBehaviour
{
    new private Rigidbody2D rigidbody2D;

    void Start() {
        rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var dir = rigidbody2D.velocity;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rigidbody2D.MoveRotation(angle);
    }
}
