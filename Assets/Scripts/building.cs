using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : PlayerObject
{
    public float health = 100;
    private string nameStr = "Base";
    public int team_base;
    public GameObject myPrefab;

    void Start()
    {
        team = team_base;
    }

    void OnMouseDown ()
    {
        GameObject Bot = Instantiate(myPrefab, transform.position + new Vector3(GetComponent<BoxCollider2D>().size.x*0.5f + 0.5f, 0, 0), Quaternion.identity);
        Bot.GetComponent<Unit>().team = team;
    }
    
    public override void TakeDamage(float damage) {
        health -= damage;
        if (health < 0)
            Destroy(gameObject);
    }
}
