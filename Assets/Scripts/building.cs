using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class building : MonoBehaviour
{
    public float health = 100;
    private string name = "Base";
    public GameObject myPrefab;

    void Start()
    {
        Debug.Log(GetComponent<BoxCollider2D>().size);
    }

    void OnMouseDown ()
    {
        Instantiate(myPrefab, transform.position + new Vector3(GetComponent<BoxCollider2D>().size.x*0.5f + 0.5f, 0, 0), Quaternion.identity);
    }
}
