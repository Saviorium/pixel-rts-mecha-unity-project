using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float damage = 1;
    private float speed = 1000f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter2D (Collision2D other)
    {
        if(other.gameObject.CompareTag("Bot"))
        {
            switch (Random.Range(-1,1)){
                case -1:
                {
                    other.gameObject.GetComponent<BotMove>().head = other.gameObject.GetComponent<BotMove>().head - damage; break;
                }
                case 0:
                {
                    other.gameObject.GetComponent<BotMove>().body = other.gameObject.GetComponent<BotMove>().body - damage; break;
                }
                case 1:
                {
                    other.gameObject.GetComponent<BotMove>().legs = other.gameObject.GetComponent<BotMove>().legs - damage; break;
                }
            }
            Destroy(gameObject);
        }
        else if(other.gameObject.CompareTag("base"))
        {other.gameObject.GetComponent<building>().health = other.gameObject.GetComponent<building>().health - damage; Destroy(gameObject);}
    }

    public void MoveToTarget(Vector2 target)
    {
        Debug.Log(target);
        GetComponent<Rigidbody2D>().velocity = target * speed * Time.deltaTime;
    }

}
