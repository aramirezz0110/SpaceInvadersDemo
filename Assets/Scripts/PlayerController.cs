using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    #endregion

    #region References    
    [Header("References")]
    [SerializeField] private Transform playerOrigin;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform laserOrigin;

    #endregion

    //TODO: limit movement using screen size
    #region Unity Methods
    private void Start()
    {
        gameObject.transform.position = playerOrigin.position;
        horizontalLimit = GameManager.Instance.horizontalLimit;
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
    #endregion
    #region Unity Callback

    #endregion
}
