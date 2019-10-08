using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float damage;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(1.0f, 0) * (speed);
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
        Debug.Log(other.gameObject.tag);
    }

}
