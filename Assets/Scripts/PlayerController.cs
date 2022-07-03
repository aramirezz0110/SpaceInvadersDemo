using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    #region Player Variables
    [Header("Variables")]
    [SerializeField] private float speed =5f;
    [SerializeField] private float speedMultiplier = 2;
    [SerializeField] private float fireRate = 0.5f;
    private float canFire = -1f;

    private float horizontalLimit;
    private float horizontalInput;
    private Vector3 direction;

    private bool isMultipleShootActive;
    private bool isSpeedBoosterActive;
    private bool isShieldActive;

    private bool isMovingLeft;
    private bool isMovingRight;  
    private bool isShooting;

    [SerializeField] private bool isMobilePlatform;

    #endregion

    #region References    
    [Header("Player References")]
    [SerializeField] private Transform playerOrigin;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform centralLaserOrigin;
    [SerializeField] private Transform leftLaserOrigin;
    [SerializeField] private Transform rightLaserOrigin;
    [SerializeField] private GameObject shieldVisualizer;
    [Header("Audio References")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootAudioClip;
    #endregion

    #region Unity Methods
    private void Start()
    {
        SetMyScaleFactor();
        shieldVisualizer.SetActive(false);
        gameObject.transform.position = playerOrigin.position;
        horizontalLimit = GameManager.Instance.horizontalLimit;
        audioSource.clip = shootAudioClip;
    }
    private void Update()
    {        
        CalculateMovement();
        FireAction();
    }
    #endregion
    #region Public Methods
    public void ActivateTripleShoot()
    {
        isMultipleShootActive = true;
        StartCoroutine(MultipleShootDownRoutine(5));
    }
    public void ActivateSpeedBooster()
    {
        isSpeedBoosterActive = true;
        speed *= speedMultiplier;
        StartCoroutine(SpeedBoosterDownRoutine(5));
    }
    public void ActivateShield()
    {
        isShieldActive = true;
        shieldVisualizer.SetActive(true);
    }
    #endregion
    #region Private Methods
    private void CalculateMovement()
    {        
        horizontalInput = Input.GetAxis("Horizontal");
        direction = new Vector3(horizontalInput, 0, 0);
        transform.Translate(direction * speed * Time.deltaTime);

        if (transform.position.x >= horizontalLimit)
        {
            transform.position = new Vector3(horizontalLimit, transform.position.y, 0);
        }
        else if (transform.position.x <= -horizontalLimit)
        {
            transform.position = new Vector3(-horizontalLimit, transform.position.y, 0);
        }
    }
    private void FireAction()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (Time.time > canFire)
            {
                if (isMultipleShootActive)
                {
                    MultipleShoot();
                }
                else
                {
                    SimpleShoot();
                }
                audioSource.Play();
            }
        }
    }
    private void SimpleShoot()
    {
        canFire = Time.time + fireRate;
        Instantiate(laserPrefab, centralLaserOrigin.position, Quaternion.identity);
    }
    private void MultipleShoot()
    {
        canFire = Time.time + fireRate;
        Instantiate(laserPrefab, centralLaserOrigin.position, Quaternion.identity);
        Instantiate(laserPrefab, leftLaserOrigin.position, Quaternion.identity);
        Instantiate(laserPrefab, rightLaserOrigin.position, Quaternion.identity);
    }  
    
    private void SetMyScaleFactor()
    {
        float tempScale = GameManager.Instance.levelSettings.scaleFactor;
        float xScale = transform.localScale.x - tempScale;
        float yScale = transform.localScale.y - tempScale;
        float zScale = transform.localScale.z - tempScale;

        transform.localScale = new Vector3(xScale, yScale, zScale);
    }
    private IEnumerator MultipleShootDownRoutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isMultipleShootActive = false;
    }
    private IEnumerator SpeedBoosterDownRoutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isSpeedBoosterActive = false;
        speed /= speedMultiplier;
    }
    #endregion
}
