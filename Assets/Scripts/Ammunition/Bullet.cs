using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damageToObjects = 0f;
    private float damageToUnits = 0f;
    private float speed = 0f;
    public GameObject explosionPrefab;

    void OnDestroy()
    {
        if(explosionPrefab != null) {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 1);
        }
    }

    void OnCollisionEnter2D (Collision2D other)
    {
        other.gameObject.GetComponent<PlayerObject>().TakeDamage(damageToUnits);
        Destroy(gameObject);
    }

    public void MoveToTarget(Vector2 target)
    {
        GetComponent<Rigidbody2D>().velocity = target * speed;
    }

    public void AttackTarget(Vector3 direction, float distance, float speed, float damageToObjects, float damageToUnits)
    {
        this.damageToUnits   = damageToUnits;
        this.damageToObjects = damageToObjects;
        this.speed = speed;
        GetComponent<Rigidbody2D>().velocity = direction * speed;
        Destroy(gameObject, distance / speed);
    }

}
