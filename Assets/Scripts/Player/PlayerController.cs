using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInputHandler _playerInputHandler;
    [SerializeField] PlayerMovementController _playerMovementController;
    [SerializeField] private PlayerShootingController _playerShootingController;

    private void Awake()
    {   
        _playerInputHandler.MoveEvent += OnMove;
        _playerInputHandler.JumpEvent += OnJump;
        _playerInputHandler.ShootEvent += OnShoot;
    }

    private void OnMove(Vector3 direction)
    {
        _playerMovementController.Move(direction);  
    }

    private void OnJump()
    {
        _playerMovementController.Jump();
    }

    private void OnShoot()
    {
        _playerShootingController.Shoot();
    }
}
