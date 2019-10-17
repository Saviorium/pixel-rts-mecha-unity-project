using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public string nameStr = "Unnamed Unit";
    public int team;

    protected SpriteRenderer selectionSprite;

    protected Vector3 moveTarget;
    protected GameObject attackTarget;

    protected RelationStorage relationWatcher;

    protected abstract void Update();

    public abstract void SetMoveTarget(Vector3 target);

    public abstract void SetAttackTarget(GameObject target);

    protected abstract void InitSelectionBorder();

    public virtual void SetSelection(bool isSelected) {
        selectionSprite.enabled = isSelected;
    }

    public virtual void Die() {
        Destroy(gameObject);
    }

    protected abstract RaycastHit2D GetHit();

    public abstract void TakeDamage(float damage);
}
