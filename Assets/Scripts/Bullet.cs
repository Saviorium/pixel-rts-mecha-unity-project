using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float damage = 1;
    private float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter2D (Collision2D other)
    {
        if(other.gameObject.CompareTag("Unit"))
        {
            other.gameObject.GetComponent<Unit>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if(other.gameObject.CompareTag("Building"))
        {other.gameObject.GetComponent<Building>().health = other.gameObject.GetComponent<Building>().health - damage; Destroy(gameObject);}
    }

    public void MoveToTarget(Vector2 target)
    {
        Debug.Log(target);
        GetComponent<Rigidbody2D>().velocity = target * speed;
    }

    public void AttackTarget(Vector3 direction, float damage_to_objects, float damage_to_units)
    {
        Debug.Log(direction);
        damage += damage_to_objects;
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

}
