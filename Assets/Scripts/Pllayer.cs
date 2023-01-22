using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pllayer : MonoBehaviour
{
    [SerializeField] private GameObject[] _items;
    
    private PlayerControlls _input;
    private PlayerAnimController _animController;
    
    private int _currentItem = 0;
 
    void Awake()
    {
        _input = new PlayerControlls();
        _input.Player.SetAnim1.started += ActivateFirstAnim;
        _input.Player.SetAnim2.started += ActivateSecondAnim;
        _input.Player.SetAnim3.started += ActivatehirdAnim;
        _input.Player.SetAnim4.started += ActivateForthAnim;
        _input.Player.SetAnim5.started += ActivateFifthAnim;
        _input.Player.SetAnim0.started += ActivateZeroAnim;
        
        _input.Player.ChangeWeapon.started += ChangeWeapon;

        _animController = GetComponent<PlayerAnimController>();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    #region ChangeAnimation
    private void ActivateFirstAnim(InputAction.CallbackContext context)
    {
        _animController.SetAnimState(1);
    }
    private void ActivateSecondAnim(InputAction.CallbackContext context)
    {
        _animController.SetAnimState(2);
    }
    private void ActivatehirdAnim(InputAction.CallbackContext context)
    {
        _animController.SetAnimState(3);
    }
    private void ActivateForthAnim(InputAction.CallbackContext context)
    {
        _animController.SetAnimState(4);
    }
    private void ActivateFifthAnim(InputAction.CallbackContext context)
    {
        _animController.SetAnimState(5);
    }
    private void ActivateZeroAnim(InputAction.CallbackContext context)
    {
        _animController.SetAnimState(0);
    }

    #endregion
    
    private void ChangeWeapon(InputAction.CallbackContext context)
    {
      
        if(_items.Length == 0)
            return;
        _items[_currentItem].SetActive(false);
        if (context.ReadValue<Vector2>().y > 0)
        {
           
            if (_items.Length > _currentItem + 1)
            {
                _currentItem++;
            }
            else
            {
                _currentItem = 0;
            }
        }
        if (context.ReadValue<Vector2>().y < 0)
        {
            if (_currentItem - 1 < 0)
            {
                _currentItem = _items.Length - 1;
            }
            else
            {
                _currentItem--;
            }
        }
        _items[_currentItem].SetActive(true);
       // Debug.Log((_currentItem));

    }

}
