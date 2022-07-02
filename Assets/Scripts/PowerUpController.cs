using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class PowerUpController : MonoBehaviour
{
    [SerializeField] private PowerUpType powerUpType;
    [SerializeField] private float speed=2;
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == GameTags.Player)
        {
            print("Collision with player");
            switch (powerUpType)
            {
                case PowerUpType.TripleShot:
                    {
                        collision.gameObject.GetComponent<PlayerController>().ActivateTripleShoot();
                        break;
                    }
                case PowerUpType.Speed:
                    {
                        collision.gameObject.GetComponent<PlayerController>().ActivateTripleShoot();
                        break;
                    }
                case PowerUpType.Shield:
                    {
                        collision.gameObject.GetComponent<PlayerController>().ActivateTripleShoot();
                        break;
                    }
                default: break;
            }
            
            Destroy(this.gameObject);
        }
        if(collision.gameObject.tag == GameTags.DeadZone)
        {
            Destroy(this.gameObject);
        }
    }
}
enum PowerUpType
{ 
    TripleShot,
    Speed,
    Shield
}

