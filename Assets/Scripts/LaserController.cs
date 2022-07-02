using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class LaserController : MonoBehaviour
{
    #region Variables
    [Header("Variables")]
    [SerializeField] private float speed = 8;
    
    #endregion
    #region Unity Methods
    private void Update()
    {
        Movement();
    }
    #endregion

    #region Unity Callbacks
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == GameTags.DeadZone)
        {
            Destroy(this.gameObject);
        }
        if(collision.gameObject.tag == GameTags.Enemy)
        {            
            Destroy(this.gameObject);
        }
    }
    #endregion

    #region Private Methods
    private void Movement()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
    #endregion
}
