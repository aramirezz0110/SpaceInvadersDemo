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
    GameManager gameManagerInstance;
    private int damageResistance;
    private int scoreIncreaseRef;
    //private float
    #endregion
    #region Unity Methods
    private void Start()
    {
        gameManagerInstance = GameManager.Instance;
        SettingsByRarity();
    }
    private void FixedUpdate()
    {
        Movement();
    }
    #endregion
    #region Private Methods
    private void SettingsByRarity()
    {
        if(enemyRarity == EnemyRarity.Green)
        {
            damageResistance = 1;
            speed = gameManagerInstance.levelSettings.greenEnemySpeed;
        }
        if(enemyRarity == EnemyRarity.Blue)
        {
            damageResistance = 2;
            speed = gameManagerInstance.levelSettings.blueEnemySpeed;
        }
        if(enemyRarity == EnemyRarity.Red)
        {
            damageResistance = 3;
            speed = gameManagerInstance.levelSettings.redEnemySpeed;
        }
        scoreIncreaseRef = damageResistance;
    }
    private void IncreasePlayerScore()
    {
        GameManager.Instance.IncreaseScore(scoreIncreaseRef);
    }
    private void Movement()
    {
        transform.Translate(Vector3.down *speed* Time.fixedDeltaTime);        
    }
    private void TakeDamage()
    {        
        --damageResistance;        
        if (damageResistance == 0)
        {
            IncreasePlayerScore();
            Destroy(this.gameObject);            
        }
    }
    #endregion
    #region Unity Callbacks
    private void OnTriggerEnter2D(Collider2D other)
    {        
        if(other.gameObject.tag == GameTags.Laser)
        {
            TakeDamage();
        }
        if(other.gameObject.tag == GameTags.DeadZone)
        {
            Destroy(this.gameObject);
        }
        if(other.gameObject.tag == GameTags.Player)
        {
            Destroy(other.gameObject);
            GameManager.Instance.SetGameOver();
        }
    }
    #endregion
}
