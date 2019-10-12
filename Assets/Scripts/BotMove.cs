using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMove : MonoBehaviour
{
    public float head = 100;
    public float body = 100;
    public float legs = 100;

    public string name = "Bot-4000";

    public float moveSpeed = 100f;
    private Vector3 moveTarget = Vector3.zero;

    private bool isSelected = false;
    private SpriteRenderer selectionSprite;

    public Sprite selectionBorderImage;

    void Start()
    {
        InitSelectionBorder();
    }

    void OnMouseDown ()
    {
        HandleSelect();
    }

    void Update()
    {
        if ((Input.GetMouseButtonDown(1)) && (isSelected)) {
            moveTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moveTarget.Scale(new Vector3(1f, 1f, 0f));
        }

        Vector3 travelVector = moveTarget - transform.position;       
        if(travelVector.magnitude > 0.1f) {    
            GetComponent<Rigidbody2D>().velocity = travelVector.normalized * moveSpeed * Time.deltaTime;
        }else if (travelVector.magnitude < 0.1f) {
            GetComponent<Rigidbody2D>().velocity = travelVector * moveSpeed * Time.deltaTime;
            moveTarget = transform.position;
        }
    }

    void InitSelectionBorder() {
        GameObject selectionBorder = new GameObject("Select Border");
        selectionBorder.transform.SetParent(this.transform);
        selectionBorder.transform.localPosition = Vector3.zero;
        selectionSprite = selectionBorder.AddComponent<SpriteRenderer>();
        selectionSprite.sortingLayerName = "UI";
        selectionSprite.sprite = selectionBorderImage;
        selectionSprite.enabled = isSelected;
    }

    void HandleSelect() {
        isSelected = !isSelected;
        selectionSprite.enabled = isSelected;
    }
}
