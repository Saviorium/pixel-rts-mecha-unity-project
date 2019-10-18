using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : UnitModule
{
    public float damageToObjects;
    public float damageToUnits;
    public float fireRate;
    public float range;
    public GameObject ammo;

    private float shotLostTime = 0f; //TODO: add Bullet obj - it should remember it's time, not the gun

    public bool isAgainstSurface;
    public bool isAgainstAir;

    public void Fire(Vector3 direction, Vector3 position)
    {
        
        RaycastHit2D  hit = Physics2D.Raycast(position + direction,
                                              new Vector2(direction.x, direction.y),
                                              range);

        if (hit.collider != null && Time.time > (shotLostTime + 1/fireRate))
        {
            GameObject bullet = Instantiate(ammo, position + direction, Quaternion.identity);
            bullet.GetComponent<Bullet>().AttackTarget(direction, damageToObjects, damageToUnits);
            shotLostTime = Time.time;
        }
    }
}
