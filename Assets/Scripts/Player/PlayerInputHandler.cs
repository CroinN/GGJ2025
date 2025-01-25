using System;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public event Action<Vector3> MoveEvent;
    public event Action JumpEvent;
    public event Action ShootEvent;
    
    private InputProvider _inputProvider;

    private void Start()
    {
        _inputProvider = SL.Get<InputProvider>();
        _inputProvider.MoveEvent += OnMove;
        _inputProvider.JumpEvent += OnJump;
        _inputProvider.ShootEvent += OnShoot;
    }

    private void OnMove(Vector3 direction)
    {
        MoveEvent?.Invoke(direction);
    }

    private void OnJump()
    {
        JumpEvent?.Invoke();
    }

    private void OnShoot()
    {
        ShootEvent?.Invoke();
    }
}
