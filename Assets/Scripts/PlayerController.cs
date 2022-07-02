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
    [SerializeField] private float speed = 10f;
    [SerializeField] private float fireRate = 0.5f;
    private float canFire = -1f;

    private float horizontalLimit;
    private float horizontalInput;
    private Vector3 direction;
    private bool isMultipleShoot;
    [SerializeField] private bool isMobilePlatform;

    #endregion

    #region References    
    [Header("Player References")]
    [SerializeField] private Transform playerOrigin;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform laserOrigin;

    [Header("Mobile Game Controller Canvas")]
    [SerializeField] private GameObject mobileControllersContainer;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRigthButton;
    [SerializeField] private Button shotButton;
    #endregion

    //TODO: limit movement using screen size
    #region Unity Methods
    private void Start()
    {
        SetMyScaleFactor();

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
        if (isMobilePlatform)
        {
            
        }
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
            SimpleShoot();
        }
    }
    private void SimpleShoot()
    {
        if (Time.time > canFire)
        {
            canFire = Time.time + fireRate;
            Instantiate(laserPrefab, laserOrigin.position, Quaternion.identity);
        }
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
    #endregion
    #region Unity Callback

    #endregion
}
