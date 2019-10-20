using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float damage = 1;
    private float speed = 10f;
    

    void OnCollisionEnter2D (Collision2D other)
    {
        other.gameObject.GetComponent<PlayerObject>().TakeDamage(damage);
        Destroy(gameObject);
    }

    public void MoveToTarget(Vector2 target)
    {
        GetComponent<Rigidbody2D>().velocity = target * speed;
    }

    public void AttackTarget(Vector3 direction, float Distance, float damage_to_objects, float damage_to_units)
    {
        damage += damage_to_units;
        GetComponent<Rigidbody2D>().velocity = direction * speed;
        Destroy(gameObject, Distance / speed);
    }

}
