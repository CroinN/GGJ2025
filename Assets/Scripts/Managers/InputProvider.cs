using System;
using Unity.VisualScripting;
using UnityEngine;

public class InputProvider : MonoBehaviour, IService
{
    public event Action<Vector3> MoveEvent;
    public event Action JumpEvent;
    public event Action ShootEvent;
    
    [SerializeField] private KeyCode _moveForwardKey = KeyCode.W;
    [SerializeField] private KeyCode _moveBackwardKey = KeyCode.S;
    [SerializeField] private KeyCode _moveLeftKey = KeyCode.A;
    [SerializeField] private KeyCode _moveRightKey = KeyCode.D;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private MouseButton _shootingButton = MouseButton.Left;

    private void Awake()
    {
        RegisterService();
    }

    private void OnDestroy()
    {
        UnregisterService();
    }

    private void Update()
    {
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        Vector3 direction = Vector3.zero;
    
        direction += Input.GetKey(_moveForwardKey) ? Vector3.forward : Vector3.zero;
        direction += Input.GetKey(_moveBackwardKey) ? Vector3.back : Vector3.zero;
        direction += Input.GetKey(_moveLeftKey) ? Vector3.left : Vector3.zero;
        direction += Input.GetKey(_moveRightKey) ? Vector3.right : Vector3.zero;
        
        bool shouldJump = Input.GetKeyDown(_jumpKey);
        
        MoveEvent?.Invoke(direction);
        
        if (shouldJump)
        {
            JumpEvent?.Invoke();
        }

        if (Input.GetMouseButton((int)_shootingButton))
        {
            ShootEvent?.Invoke();
        }
    }

    public void RegisterService()
    {
        SL.Register(this);
    }

    public void UnregisterService()
    {
        SL.Unregister(this);
    }
}
