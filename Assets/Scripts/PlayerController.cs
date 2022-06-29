using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region References
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform laserOrigin;
    [SerializeField] private InputActionReference movementAction;
    [SerializeField] private InputActionReference shootAction;
    #endregion

    #region Player Variables
    [Header("Variables")]
    [SerializeField] private float speed = 4f;
    [SerializeField] private float fireRate = 0.5f;
    private float canFire = -1f;
    #endregion
    //TODO: limit movement using screen size
    #region Unity Methods
    private void OnEnable()
    {
        movementAction.action.performed += Movement;
        shootAction.action.performed += Shoot;
    }
    private void OnDisable()
    {
        movementAction.action.performed -= Movement;
        shootAction.action.performed -= Shoot;
    }
    private void Movement(InputAction.CallbackContext obj)
    {
        print(obj.ReadValue<int>());
    }
    private void Shoot(InputAction.CallbackContext obj)
    {
        print(obj.ReadValue<bool>());        
    }

    

    
    #endregion
}
