using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class EnemyController : MonoBehaviour
{
    #region Variables
    [Header("Enemy Variables")]
    [SerializeField] private EnemyRarity enemyRarity;
    [SerializeField] private float speed = 4.0f;
    private int damageResistance;
    //private float
    #endregion
    #region Unity Methods
    private void Start()
    {
        SetDamageResistance();
    }
    private void Update()
    {
        Movement();
    }
    #endregion
    #region Private Methods
    private void SetDamageResistance()
    {
        if(enemyRarity == EnemyRarity.Green)
        {
            damageResistance = 1;
        }
        if(enemyRarity == EnemyRarity.Blue)
        {
            damageResistance = 2;
        }
        if(enemyRarity == EnemyRarity.Red)
        {
            damageResistance = 3;
        }
    }
    private void IncreasePlayerScore()
    {
        if(enemyRarity == EnemyRarity.Green)
        {
            GameManager.Instance.IncreaseScore(1);
        }
        if(enemyRarity == EnemyRarity.Blue)
        {
            GameManager.Instance.IncreaseScore(2);
        }
        if(enemyRarity == EnemyRarity.Red)
        {
            GameManager.Instance.IncreaseScore(3);
        }
    }
    private void Movement()
    {
        transform.Translate(Vector3.down * speed* Time.deltaTime);        
    }
    private void TakeDamageByRarity()
    {
        damageResistance--;
        if (damageResistance == 0)
        {
            Destroy(this.gameObject);
            IncreasePlayerScore();
        }
    }
    #endregion
    #region Unity Callbacks
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == GameTags.Player)
        {
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == GameTags.Laser)
        {
            TakeDamageByRarity();
        }
        if(other.gameObject.tag == GameTags.DeadZone)
        {
            Destroy(this.gameObject);
        }
    }
    #endregion
}
