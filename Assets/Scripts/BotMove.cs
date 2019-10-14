using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMove : MonoBehaviour
{
    public string name = "Bot-4000";

    public float head = 100;
    public float body = 100;
    public float legs = 100;
    public float attackRange = 8f;

    public float moveSpeed = 100f;
    public int team;

    public SpriteRenderer selectionSprite;
    public Sprite selectionBorderImage;

    public GameObject ammo;

    private Vector3 moveTarget;
    private GameObject attackTarget;
    private bool isSelected = false;
    private float ShotLostTime;

    void Start()
    {
        InitSelectionBorder();
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
                                                  new Vector2( 
                                                          attackVector.x, 
                                                          attackVector.y
                                                         ),
                                                  attackRange);
            if (hit.collider != null)
                if (attackVector.magnitude <= attackRange && hit.collider.gameObject == attackTarget && Time.time > (ShotLostTime + 5))
                {
                    GameObject bullet = Instantiate(ammo, (Vector3) transform.position + attackVector, Quaternion.identity);
                    bullet.GetComponent<Bullet>().MoveToTarget(attackVector);
                    ShotLostTime = Time.time;
                }
        }
    }

    public void SetMoveTarget(Vector3 target)
    {
        moveTarget = target;
        moveTarget.Scale(new Vector3(1f, 1f, 0f));
    }

    public void SetAttackTarget(GameObject target)
    {
        bool IsEnemy = GameObject.Find("RelationWatcher").GetComponent<RelationStorage>().IsEnemy(gameObject, target);
        if (IsEnemy)
            attackTarget = target;
    }

    void InitSelectionBorder() {
        GameObject selectionBorder = new GameObject("Select Border");
        selectionBorder.transform.SetParent(this.transform);
        selectionBorder.transform.localPosition = Vector3.zero;
        selectionSprite = selectionBorder.AddComponent<SpriteRenderer>();
        selectionSprite.sortingLayerName = "UI";
        selectionSprite.sprite = selectionBorderImage;
        selectionSprite.enabled = false;
    }

    RaycastHit2D GetHit()
    {
        var mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D  hit = Physics2D.Raycast(new Vector2( 
                                                          mouse_pos.x, 
                                                          mouse_pos.y
                                                         ),
                                              Vector2.zero, 0);
        return hit;
    }
}
