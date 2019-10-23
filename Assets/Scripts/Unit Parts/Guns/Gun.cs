using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : UnitModule
{
    public float damageToObjects;
    public float damageToUnits;
    public float fireRate;
    public float range;
    protected float accuracy;
    public GameObject ammo;

    private float shotLostTime = 0f; //TODO: add Bullet obj - it should remember it's time, not the gun

    public bool isAgainstSurface;
    public bool isAgainstAir;

    public void Fire(Vector3 direction, Vector3 position)
    {
        float Distance = Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2));
        Vector3 Direction = direction.normalized;
        var relationWatcher = GameObject.Find("RelationWatcher").GetComponent<RelationStorage>();
        RaycastHit2D  hit = Physics2D.Raycast(position + Direction,
                                              new Vector2(Direction.x, Direction.y),
                                              range);

        if (Time.time > (shotLostTime + 1/fireRate))
            if (hit.collider != null){
                if (relationWatcher.IsEnemy(transform.parent.gameObject, hit.collider.gameObject))
                {
                    Shoot_ammo(GetCalculatedVector(Direction), position, Distance);
                }
            }
            else
            {
                Shoot_ammo(GetCalculatedVector(Direction), position, Distance);
            }
    }

    void Shoot_ammo (Vector3 direction, Vector3 position, float Distance)
    {
        GameObject bullet = Instantiate(ammo, position + direction, Quaternion.identity);
        bullet.GetComponent<Bullet>().AttackTarget(direction, Distance + Random.Range(-accuracy, accuracy)*10, damageToObjects, damageToUnits);
        shotLostTime = Time.time;
    }

    Vector3 GetCalculatedVector(Vector3 Direction )
    {
        // Построение вектора который отклоняется от вектора между ботом-целью на +- accuracy, пока что эффективно бот стреляет при accuracy 0.08f
        // При точности 0.15 уже творится веселье)
        return new Vector3(Direction.x - (Direction.y * Random.Range(-accuracy, accuracy))/Mathf.Sqrt(Mathf.Pow(Direction.x, 2) + Mathf.Pow(Direction.y, 2)), 
                           Direction.y + (Direction.x * Random.Range(-accuracy, accuracy))/Mathf.Sqrt(Mathf.Pow(Direction.x, 2) + Mathf.Pow(Direction.y, 2)),
                           0);
    }
}
