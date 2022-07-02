using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class PowerUpController : MonoBehaviour
{
    [SerializeField] private float speed=2;
    [SerializeField] private PlayerController playerController;
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == GameTags.Player)
        {
            print("Collision with player");
            collision.gameObject.GetComponent<PlayerController>().ActivateTripleShoot();
            Destroy(this.gameObject);
        }
        if(collision.gameObject.tag == GameTags.DeadZone)
        {
            Destroy(this.gameObject);
        }
    }
}
