using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    protected float damage_to_objects;
    protected float damage_to_units;
    protected float rate_of_fire;
    protected float range;
    protected GameObject Ammo;

    protected float ShotLostTime;

    protected bool IsAgainstSurface;
    protected bool IsAgainstAir;

    public void Fire(Vector3 direction, Vector3 position)
    {
        
        RaycastHit2D  hit = Physics2D.Raycast(position + direction,
                                              new Vector2(direction.x, direction.y),
                                              range);

        if (hit.collider != null && Time.time > (ShotLostTime + 1/rate_of_fire))
        {
                GameObject bullet = Instantiate(Ammo, position + direction, Quaternion.identity);
                bullet.GetComponent<Bullet>().AttackTarget(direction, damage_to_objects, damage_to_units);
                ShotLostTime = Time.time;
        }
    }

    public void SetAmmo(GameObject ammo)
    {
        Ammo = ammo;
    }
}
