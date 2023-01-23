using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private GameObject[] _items;
    [SerializeField] private GameObject _camera;
    
    [SerializeField] private float _rotationSmoothTime = 0.12f;
    [SerializeField] private bool _activateInputOnAwake;
    
    
    private PlayerControlls _input;
    private PlayerAnimController _animController;
    private Rigidbody _rigidbody;
    
    private Vector2 _movementInputAxis;
    private Vector2 _inputLookAxis;
    
    private float _rotationVelocity;
    private int _currentItem = 0;
    private bool _isFalling = false;
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
        if (!_rigidbody)
        {
            _rigidbody = gameObject.AddComponent<Rigidbody>();
        }
        
        _animController = GetComponent<PlayerAnimController>();
        
        if (!_animController)
        {
            _animController = gameObject.AddComponent<PlayerAnimController>();
        }
        
        _input = new PlayerControlls();
        _input.Player.ChangeWeapon.started += ChangeWeapon;
        _input.Player.Move.performed += MoveInput;
        _input.Player.Move.canceled += StopMove;
        _input.Player.Look.performed += RotateInput;
    }
    
    private void FixedUpdate()
    {
        Move();
    }
    

    private void OnEnable()
    {
        if(_activateInputOnAwake)
            _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }
    
    public void DisableInput()
    {
        _input.Disable();
        
    }

    public void EnableInput()
    {
        _input.Enable();
    }
    
    private void Move()
    {
        var accelerate = _isFalling ? 0 : _speed;
        
        var forward = Vector3.ProjectOnPlane(_camera.transform.forward, Vector3.up);
        var right = Vector3.ProjectOnPlane(_camera.transform.right, Vector3.up);
        
        var direction = forward.normalized * _movementInputAxis.y + right.normalized * _movementInputAxis.x;
        
        Vector3 inputDirection = new Vector3(_movementInputAxis.x, 0.0f, _movementInputAxis.y).normalized;

        float  targetRotation = 0.0f;
 
        if (!direction.Equals(Vector3.zero))
        {
            targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                             _camera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _rotationVelocity,
                _rotationSmoothTime);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            
            Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
     
            _rigidbody.velocity = new Vector3(targetDirection.x * accelerate, _rigidbody.velocity.y, targetDirection.z * accelerate);
            if (_animController)
            {
                _animController.SetAnimState(1);
            }
        }
        else
        {
            
            _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y, 0);
            
            if (_animController)
            {
                _animController.SetAnimState(0);
            }
        }
    }
    
    private void MoveInput(InputAction.CallbackContext context)
    {
        _movementInputAxis = context.ReadValue<Vector2>();
       }
    
    public void StopMove(InputAction.CallbackContext context)
    {
        _movementInputAxis = Vector2.zero;
    }
    
    private void RotateInput(InputAction.CallbackContext context)
    {
        _inputLookAxis = context.ReadValue<Vector2>();
        Debug.Log(_inputLookAxis);
    }
    
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
    }
}
