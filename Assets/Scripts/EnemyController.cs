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
    private bool instancePowerUp;
    //private float
    #endregion
    #region Unity Methods
    private void Start()
    {
        gameManagerInstance = GameManager.Instance;
        SettingsByRarity();
        CalculatePowerUpSpawnProbability();
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
        }
        else if(transform.position.y<topSectionLimit && transform.position.y<secondSectionLimit)
        {
            GameManager.Instance.IncreaseScore(scoreIncreaseRef * 2);            
        }
        else if (transform.position.y<secondSectionLimit)
        {
            GameManager.Instance.IncreaseScore(scoreIncreaseRef);           
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
            if (instancePowerUp)
            {
                int randomPowerUp = Random.Range(0, gameManagerInstance.powerUps.Count);
                Instantiate(gameManagerInstance.powerUps[randomPowerUp], transform.position, transform.rotation);
            }
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
            GameManager.Instance.SetGameOver();
            Destroy(other.gameObject);            
        }
    }
    private void CalculatePowerUpSpawnProbability()
    {
        int powerUpProbability = Random.Range(0,11);
        if (powerUpProbability != 0)
        {
            if (enemyRarity == EnemyRarity.Green)
            {
                if (powerUpProbability == 1)
                    instancePowerUp = true;
            }
            if (enemyRarity == EnemyRarity.Blue)
            {
                if (powerUpProbability == 1 || powerUpProbability == 2)
                    instancePowerUp = true;
            }
            if (enemyRarity == EnemyRarity.Red)
            {
                if (powerUpProbability > 0 && powerUpProbability < 4)
                    instancePowerUp = true;
            }
        }
        else
        {
            instancePowerUp = false;
        }
    }
    #endregion
}
