using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _moveSpeed = 1f;

    private Vector3 _moveDirection = Vector3.zero;

    private void Update()
    {
        if(_moveDirection != Vector3.zero)
        {
            _playerTransform.Translate(_moveDirection * Time.deltaTime * _moveSpeed);
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
