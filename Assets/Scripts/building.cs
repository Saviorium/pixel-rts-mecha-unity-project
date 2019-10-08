using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class building : MonoBehaviour
{
    private float health;
    private string name;
    public GameObject myPrefab;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        name = "Base";
        Debug.Log(GetComponent<BoxCollider2D>().size);
    }

    void OnMouseDown ()
    {
        Instantiate(myPrefab, transform.position + new Vector3(GetComponent<BoxCollider2D>().size.x*0.5f + 0.5f, 0, 0), Quaternion.identity);
    }
}
