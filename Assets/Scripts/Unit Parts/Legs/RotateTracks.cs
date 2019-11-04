using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTracks : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer sprite;
    private float direction = 0f;
    private Rigidbody2D botRB;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        sprite = this.GetComponent<SpriteRenderer>();
        botRB = this.GetComponentInParent<Unit>().gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (botRB.velocity.sqrMagnitude > 0.1f) {
            Vector2 velocity = botRB.velocity.normalized;
            direction = 90 - Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            TurnTo(direction);
            animator.SetFloat("speed", 1f);
        } else {
            animator.SetFloat("speed", 0f);
        }
    }

    private void TurnTo(float directionDeg) {
        directionDeg = NormalizeDeg(directionDeg + 22.5f); //rotate CW for deg (-22.5 - 0)
        int directionId = (int)(directionDeg/45f);
        //7 0 1
        //6   2
        //5 4 3
        if (directionId == 1 || directionId == 5) {
            sprite.flipX = true;
        } else {
            sprite.flipX = false;
        }
        animator.SetInteger("direction", directionId);
    }

    private float NormalizeDeg(float angle) {
        return Mathf.Repeat(angle, 360f);
    }
}
