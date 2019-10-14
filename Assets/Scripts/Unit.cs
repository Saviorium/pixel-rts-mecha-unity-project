using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string nameStr = "Unnamed Unit";

    public float health = 100f;

    public float attackRange = 8f;
    public float attackSpeed = 1f; //bullets per sec

    public float moveSpeed = 100f;
    public int team;

    private SpriteRenderer selectionSprite;

    public GameObject ammo;

    private Vector3 moveTarget;
    private GameObject attackTarget;
    private float ShotLostTime;

    private RelationStorage relationWatcher;

    void Start()
    {
        InitSelectionBorder();
        relationWatcher = GameObject.Find("RelationWatcher").GetComponent<RelationStorage>();
    }

    void Update()
    {
        
        Vector3 travelVector = moveTarget - transform.position;       
        if(travelVector.magnitude > 0.1f) {    
            GetComponent<Rigidbody2D>().velocity = travelVector.normalized * moveSpeed * Time.deltaTime;
        }else if (travelVector.magnitude < 0.1f) {
            GetComponent<Rigidbody2D>().velocity = travelVector * moveSpeed * Time.deltaTime;
            moveTarget = transform.position;
        }
        if (attackTarget != null ){
            Vector3 attackVector = (attackTarget.transform.position - transform.position).normalized;
            RaycastHit2D  hit = Physics2D.Raycast(transform.position + attackVector,
                                                  new Vector2(attackVector.x, attackVector.y),
                                                  attackRange);
            if ((hit.collider != null) && (attackVector.magnitude <= attackRange && hit.collider.gameObject == attackTarget && Time.time > (ShotLostTime + 1/attackSpeed))){
                GameObject bullet = Instantiate(ammo, (Vector3) transform.position + attackVector, Quaternion.identity);
                bullet.GetComponent<Bullet>().MoveToTarget(attackVector);
                ShotLostTime = Time.time;
            }
        }
    }

    public void SetMoveTarget(Vector3 target)
    {
        moveTarget = target;
        moveTarget.z = 0f;
    }

    public void SetAttackTarget(GameObject target)
    {
        bool isEnemy = relationWatcher.IsEnemy(gameObject, target);
        if (isEnemy)
            attackTarget = target;
    }

    void InitSelectionBorder() {
        GameObject selectionBorder = (GameObject)Instantiate(Resources.Load("Selection Box"));;
        selectionBorder.transform.SetParent(this.transform);
        selectionBorder.transform.localPosition = Vector3.zero;
        selectionSprite = selectionBorder.GetComponent<SpriteRenderer>();
        selectionSprite.enabled = false;
    }

    public virtual void SetSelection(bool isSelected) {
        selectionSprite.enabled = isSelected;
    }

    public virtual void TakeDamage(float damage) {
        health -= damage;
        if(health < 0f) {
            Die();
        }
    }

    public virtual void Die() {
        Destroy(gameObject);
    }

    RaycastHit2D GetHit()
    {
        var mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D  hit = Physics2D.Raycast(
            new Vector2(mouse_pos.x, mouse_pos.y),
            Vector2.zero,
            0);
        return hit;
    }
}
