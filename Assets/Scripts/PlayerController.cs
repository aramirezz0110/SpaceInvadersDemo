using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

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

    [Header("Mobile Game Controller Canvas")]
    [SerializeField] private GameObject mobileControllersContainer;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRigthButton;
    [SerializeField] private Button shotButton;
    #endregion

    
    #region Unity Methods
    private void Start()
    {
        SetMyScaleFactor();
        shieldVisualizer.SetActive(false);

        gameObject.transform.position = playerOrigin.position;
        horizontalLimit = GameManager.Instance.horizontalLimit;

        if (GameManager.Instance.IsMobilePlatform())
        {            
            AddMobileControllsListeners();
        }
        
    }
    private void Update()
    {
        CalculateMovement();
        FireAction();
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
                    MultipleShot();
                }
                else
                {
                    SimpleShot();
                }
            }
        }
    }
    private void SimpleShot()
    {
        canFire = Time.time + fireRate;
        Instantiate(laserPrefab, centralLaserOrigin.position, Quaternion.identity);
    }
    private void MultipleShot()
    {
        canFire = Time.time + fireRate;
        Instantiate(laserPrefab, centralLaserOrigin.position, Quaternion.identity);
        Instantiate(laserPrefab, leftLaserOrigin.position, Quaternion.identity);
        Instantiate(laserPrefab, rightLaserOrigin.position, Quaternion.identity);
    }
    
    private void AddMobileControllsListeners()
    {
        
        moveRigthButton.onClick.AddListener(MoveRight);
        shotButton.onClick.AddListener(FireAction);
    }
    private void MoveLeft()
    {
        horizontalInput = -1;
    }
    private void MoveRight()
    {
        horizontalInput = 1;
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
    #region Unity Callback

    #endregion
}
