using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : Unit
{    
    public GameObject ammo;

    protected Unit_System system;
    protected Chassis chassis;
    protected MovingPart movingPart;
    protected Gun gun;

    void Start()
    {
        InitSelectionBorder();
        relationWatcher = GameObject.Find("RelationWatcher").GetComponent<RelationStorage>();
        //  Не знаю пока как объявить указанные в коде объекты, так что оставлю пока всё так

        chassis = new Bot_Body_1();
        movingPart = new Bot_Legs_1();
        system = new Core_1();
        gun  = new Gatling();
    }

    protected override void Update()
    {
        
        Vector3 travelVector = moveTarget - transform.position;       
        if(travelVector.magnitude > 0.1f) {    
            GetComponent<Rigidbody2D>().velocity = travelVector.normalized * movingPart.GetSpeed(chassis.GetWeigth() + movingPart.GetWeigth()) * Time.deltaTime;
        }else if (travelVector.magnitude < 0.1f) {
            GetComponent<Rigidbody2D>().velocity = travelVector * movingPart.GetSpeed(chassis.GetWeigth() + movingPart.GetWeigth()) * Time.deltaTime;
            moveTarget = transform.position;
        }

        if (attackTarget != null )
                gun.Fire((attackTarget.transform.position - transform.position).normalized, transform.position);
        
    }

    public override void SetMoveTarget(Vector3 target)
    {
        moveTarget = target;
        moveTarget.z = 0f;
    }

    public override void SetAttackTarget(GameObject target)
    {
        bool isEnemy = relationWatcher.IsEnemy(gameObject, target);
        if (isEnemy)
            attackTarget = target;
    }

    protected override void InitSelectionBorder() {
        GameObject selectionBorder = (GameObject)Instantiate(Resources.Load("Selection Box"));;
        selectionBorder.transform.SetParent(this.transform);
        selectionBorder.transform.localPosition = Vector3.zero;
        selectionSprite = selectionBorder.GetComponent<SpriteRenderer>();
        selectionSprite.enabled = false;
    }

    protected override RaycastHit2D GetHit()
    {
        var mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D  hit = Physics2D.Raycast(
            new Vector2(mouse_pos.x, mouse_pos.y),
            Vector2.zero,
            0);
        return hit;
    }

    public override void TakeDamage(float damage) {
        switch (Random.Range(0,3)){
            case 0:
            {
                system.TakeDamage(damage); break;
            }
            case 1:
            {
                chassis.TakeDamage(damage); break;
            }
            case 2:
            {
                movingPart.TakeDamage(damage); break;
            }
        }
        if(chassis.GetHp() < 0f) {
            Die();
        }
    }
}
