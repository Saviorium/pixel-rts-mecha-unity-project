using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{

    protected SpriteRenderer selectionSprite;
    protected RelationStorage relationWatcher;
    public int team;
    
    public virtual void TakeDamage(float damage)
    {
        Debug.Log(gameObject +"GetDamage");
    }

    public virtual void SetSelection(bool isSelected) 
    {
        Debug.Log(gameObject + " Selected");
    }
}
