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
    private void Update()
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
        float limitDifference = gameManagerInstance.superiorLimit - Mathf.Abs(gameManagerInstance.inferiorLimit);
        float topSectionLimit = limitDifference - limitDifference/3;
        float secondSectionLimit = limitDifference - ((limitDifference / 3) * 2);        
        if (transform.position.y< gameManagerInstance.superiorLimit && transform.position.y > topSectionLimit)
        {
            GameManager.Instance.IncreaseScore(scoreIncreaseRef*3);
            print("score x3");
        }
        else if(transform.position.y<topSectionLimit && transform.position.y<secondSectionLimit)
        {
            GameManager.Instance.IncreaseScore(scoreIncreaseRef * 2);
            print("score x2");
        }
        else if (transform.position.y<secondSectionLimit)
        {
            GameManager.Instance.IncreaseScore(scoreIncreaseRef);
            print("score x1");
        }
        
    }
    private void Movement()
    {
        transform.Translate(Vector3.down *speed* Time.deltaTime);        
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
