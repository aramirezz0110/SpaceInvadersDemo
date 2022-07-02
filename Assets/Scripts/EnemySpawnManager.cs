using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    #region References     
    [Header("Enemies References")]
    [SerializeField] private Transform enemyContainer;
    [SerializeField] private Transform enemyOrigin;
    [SerializeField] private GameObject greenEnemyPrefab;
    [SerializeField] private GameObject blueEnemyPrefab;
    [SerializeField] private GameObject redEnemyPrefab;
    #endregion
    #region Variables
    [Header("Variables")]
    [SerializeField] private float greenEnemySpawnWaitTime=2f;
    [SerializeField] private float blueEnemySpawnWaitTime=3f;
    [SerializeField] private float redEnemySpawnWaitTime=4f;
    private float scaleReductionRef;
        
    private GameManager gameManagerInstance;
    #endregion

    #region Unity Methods
    private void Start()
    {
        gameManagerInstance = GameManager.Instance;

        SetEnemiesConfig();

        StartCoroutine(GreenEnemySpawnRoutine(greenEnemySpawnWaitTime));
        StartCoroutine(BlueEnemySpawnRoutine(blueEnemySpawnWaitTime));
        StartCoroutine(RedEnemySpawnRoutine(redEnemySpawnWaitTime));
    }
    #endregion

    #region Private Methods
    private IEnumerator GreenEnemySpawnRoutine(float waitTime)
    {
        Vector3 spawingPosition;
        float randomRange;
        GameObject tempEnemyRef;
        while (!GameManager.Instance.stopSpawning)
        {
            randomRange = Random.Range(-GameManager.Instance.horizontalLimit, GameManager.Instance.horizontalLimit);
            spawingPosition = new Vector3(randomRange, enemyOrigin.position.y, 0);
            tempEnemyRef = Instantiate(greenEnemyPrefab, spawingPosition, Quaternion.identity, enemyContainer);
            RescaleEnemy(tempEnemyRef);            

            yield return new WaitForSeconds(waitTime);
        }
    }
    private IEnumerator BlueEnemySpawnRoutine(float waitTime)
    {
        Vector3 spawingPosition;
        float randomRange;
        GameObject tempEnemyRef;
        while (!GameManager.Instance.stopSpawning)
        {
            yield return new WaitForSeconds(waitTime);

            randomRange = Random.Range(-GameManager.Instance.horizontalLimit, GameManager.Instance.horizontalLimit);
            spawingPosition = new Vector3(randomRange, enemyOrigin.position.y, 0);
            tempEnemyRef = Instantiate(blueEnemyPrefab, spawingPosition, Quaternion.identity, enemyContainer);
            RescaleEnemy(tempEnemyRef);
        }
    }
    private IEnumerator RedEnemySpawnRoutine(float waitTime)
    {
        Vector3 spawingPosition;
        float randomRange;
        GameObject tempEnemyRef;
        while (!GameManager.Instance.stopSpawning)
        {
            yield return new WaitForSeconds(waitTime);

            randomRange = Random.Range(-GameManager.Instance.horizontalLimit, GameManager.Instance.horizontalLimit);
            spawingPosition = new Vector3(randomRange, enemyOrigin.position.y, 0);
            tempEnemyRef = Instantiate(redEnemyPrefab, spawingPosition, Quaternion.identity, enemyContainer);
            RescaleEnemy(tempEnemyRef);
        }
    }
    private void SetEnemiesConfig()
    {
        greenEnemySpawnWaitTime = gameManagerInstance.levelSettings.greenEnemySpawnWaitTime;
        blueEnemySpawnWaitTime = gameManagerInstance.levelSettings.blueEnemySpawnWaitTime;
        redEnemySpawnWaitTime = gameManagerInstance.levelSettings.redEnemySpawnWaitTime;

        scaleReductionRef = gameManagerInstance.levelSettings.scaleFactor;
    }
    private void RescaleEnemy(GameObject enemy)
    {
        float xScale = enemy.transform.localScale.x - scaleReductionRef;
        float yScale = enemy.transform.localScale.y - scaleReductionRef;
        float zScale = enemy.transform.localScale.z - scaleReductionRef;

        enemy.transform.localScale = new Vector3(xScale, yScale, zScale);
    }
    #endregion
}
