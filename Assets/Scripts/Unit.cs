using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public string nameStr = "Unnamed Unit";
    public int team;

    public List<UnitModule> modules;

    protected SpriteRenderer selectionSprite;

    protected Vector3 moveTarget;
    protected GameObject attackTarget;

    protected RelationStorage relationWatcher;

    void Start() {
        InitSelectionBorder();
        moveTarget = transform.position;
        relationWatcher = GameObject.Find("RelationWatcher").GetComponent<RelationStorage>();

        InitComponents();
    }

    protected abstract void InitComponents();

    protected T AddModule<T>() where T : UnitModule {
        GameObject moduleObj = new GameObject(typeof(T).ToString());
        moduleObj.transform.parent = transform;
        T module = moduleObj.AddComponent<T>();
        modules.Add(module);
        return module;
    }

    void Update()
    {
        
        Vector3 travelVector = moveTarget - transform.position;       
        if(travelVector.magnitude > 0.01f) {
            float speed = GetSpeed();
            GetComponent<Rigidbody2D>().velocity = travelVector.normalized * speed * Time.deltaTime;
        } else  {
            moveTarget = transform.position;
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }

        if (attackTarget != null ) {
            FireGuns();
        }
        
    }

    private float GetSpeed()
    {
        float speed = 0f;
        float weight = GetWeight();
        foreach (var module in modules) {
            if (module is MovingPart) {
                speed += ((MovingPart)module).GetSpeed(weight);
            }
        }
        return speed;
    }

    private float GetWeight()
    {
        float weight = 0f;
        foreach (var module in modules) {
            weight += module.weight;
        }
        return weight;
    }

    private void FireGuns()
    {
        foreach (var module in modules) {
            if (module is Gun) {
                ((Gun)module).Fire((attackTarget.transform.position - transform.position).normalized, transform.position);
            }
        }
    }

    public virtual void SetMoveTarget(Vector3 target)
    {
        moveTarget = target;
        moveTarget.z = 0f;
    }

    public virtual void SetAttackTarget(GameObject target)
    {
        bool isEnemy = relationWatcher.IsEnemy(gameObject, target);
        if (isEnemy)
            attackTarget = target;
    }

    
    protected virtual void InitSelectionBorder() {
        GameObject selectionBorder = (GameObject)Instantiate(Resources.Load("Selection Box"));
        selectionBorder.transform.SetParent(this.transform);
        selectionBorder.transform.localPosition = Vector3.zero;
        selectionSprite = selectionBorder.GetComponent<SpriteRenderer>();
        selectionSprite.enabled = false;
    }

    public virtual void SetSelection(bool isSelected) {
        selectionSprite.enabled = isSelected;
    }

    public virtual void Die() {
        Destroy(gameObject);
    }

    protected virtual RaycastHit2D GetHit()
    {
        var mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D  hit = Physics2D.Raycast(
            new Vector2(mouse_pos.x, mouse_pos.y),
            Vector2.zero,
            0);
        return hit;
    }

    public virtual void TakeDamage(float damage) {
        modules[UnityEngine.Random.Range(0, modules.Count)].TakeDamage(damage);
    }
}
