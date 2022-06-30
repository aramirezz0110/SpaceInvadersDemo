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
    

    #endregion

    #region Unity Methods
    private void Start()
    {
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
        while (!GameManager.Instance.stopSpawning)
        {
            randomRange = Random.Range(-GameManager.Instance.horizontalLimit, GameManager.Instance.horizontalLimit);
            spawingPosition = new Vector3(randomRange, enemyOrigin.position.y, 0);
            Instantiate(greenEnemyPrefab, spawingPosition, Quaternion.identity, enemyContainer);

            yield return new WaitForSeconds(waitTime);
        }
    }
    private IEnumerator BlueEnemySpawnRoutine(float waitTime)
    {
        Vector3 spawingPosition;
        float randomRange;
        while (!GameManager.Instance.stopSpawning)
        {
            yield return new WaitForSeconds(waitTime);

            randomRange = Random.Range(-GameManager.Instance.horizontalLimit, GameManager.Instance.horizontalLimit);
            spawingPosition = new Vector3(randomRange, enemyOrigin.position.y, 0);
            Instantiate(blueEnemyPrefab, spawingPosition, Quaternion.identity, enemyContainer); 
        }
    }
    private IEnumerator RedEnemySpawnRoutine(float waitTime)
    {
        Vector3 spawingPosition;
        float randomRange;
        while (!GameManager.Instance.stopSpawning)
        {
            yield return new WaitForSeconds(waitTime);

            randomRange = Random.Range(-GameManager.Instance.horizontalLimit, GameManager.Instance.horizontalLimit);
            spawingPosition = new Vector3(randomRange, enemyOrigin.position.y, 0);
            Instantiate(redEnemyPrefab, spawingPosition, Quaternion.identity, enemyContainer);    
        }
    }
    #endregion
}
