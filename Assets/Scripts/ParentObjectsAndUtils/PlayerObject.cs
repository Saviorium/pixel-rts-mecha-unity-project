using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{

    protected SpriteRenderer  selectionSprite;
    protected AttitudeStorage relationWatcher;
    public    int             team;

    protected void SetColor()
    {
        Transform  children = gameObject.GetComponentsInChildren<Transform>()[1];
        var SpriteRenderer = children.gameObject.GetComponent<SpriteRenderer>();
        SpriteRenderer.color = GameObject.Find("RelationWatcher").GetComponent<AttitudeStorage>().GetTeamColor(team);
    }
    
    public virtual void TakeDamage(float damage)
    {
        Debug.Log(gameObject +"GetDamage");
    }

    public virtual void SetSelection(bool isSelected) 
    {
        Debug.Log(gameObject + " Selected");
    }

    protected virtual void InitSelectionBorder() 
    {
        GameObject selectionBorder = (GameObject)Instantiate(Resources.Load("Selection Box"));
        selectionBorder.transform.SetParent(this.transform);
        selectionBorder.transform.localPosition = Vector3.zero;
        selectionSprite = selectionBorder.GetComponent<SpriteRenderer>();
        selectionSprite.enabled = false;
    }
}
