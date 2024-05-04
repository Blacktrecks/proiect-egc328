using System;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _playerRb;
    [SerializeField] private float _moveSpeed = 1f;

    public bool IsCutting = false;
    public event Action PlayerMovedAwayFromCuttingBoard;

    private Vector3 _moveDirection = Vector3.zero;

    private void FixedUpdate()
    {
        if (_moveDirection != Vector3.zero)
        {
            Vector3 lookDirection = new Vector3(_moveDirection.z, _moveDirection.y, -_moveDirection.x);
            _playerRb.MoveRotation(Quaternion.LookRotation(lookDirection));

            _playerRb.MovePosition(_playerRb.position + _moveSpeed * Time.fixedDeltaTime * _moveDirection);

            if (IsCutting)
            {
                IsCutting = false;
                PlayerMovedAwayFromCuttingBoard?.Invoke();
            }
        }
    }

    public void MoveUp(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            _moveDirection.x = 1.0f;
        }
        else if (context.phase == InputActionPhase.Canceled && _moveDirection.x == 1.0f)
        {
            _moveDirection.x = 0;
        }
    }

    public void MoveDown(InputAction.CallbackContext context) 
    {
        if (context.phase == InputActionPhase.Started)
        {
            _moveDirection.x = -1.0f;
        }
        else if (context.phase == InputActionPhase.Canceled && _moveDirection.x == -1.0)
        {
            _moveDirection.x = 0.0f;
        }
    }

    public void MoveLeft(InputAction.CallbackContext context) 
    {
        if (context.phase == InputActionPhase.Started)
        {
            _moveDirection.z = 1.0f;
        }
        else if (context.phase == InputActionPhase.Canceled && _moveDirection.z == 1.0)
        {
            _moveDirection.z = 0;
        }
    }

    public void MoveRight(InputAction.CallbackContext context) 
    {
        if (context.phase == InputActionPhase.Started)
        {
            _moveDirection.z = -1.0f;
        }
        else if (context.phase == InputActionPhase.Canceled && _moveDirection.z == -1.0)
        {
            _moveDirection.z = 0;
        }
    }
}
