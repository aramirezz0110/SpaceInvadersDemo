using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D),typeof(AudioSource))]
public class PowerUpController : MonoBehaviour
{
    [SerializeField] private PowerUpType powerUpType;
    [SerializeField] private float speed=2;
    [Header("Audio References")]
    [SerializeField] private AudioSource powerUpAudioSource;
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == GameTags.Player)
        {            
            switch (powerUpType)
            {
                case PowerUpType.TripleShoot:
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
            powerUpAudioSource.Play();
            Destroy(this.gameObject,.25f);
        }
        if(collision.gameObject.tag == GameTags.DeadZone)
        {
            Destroy(this.gameObject);
        }
    }
}
enum PowerUpType
{ 
    TripleShoot,
    Speed,
    Shield
}

