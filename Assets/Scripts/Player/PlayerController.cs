using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler _playerInputHandler;
    [SerializeField] private PlayerMovementController _playerMovementController;
    [SerializeField] private PlayerRotateController _playerRotateController;

    private void Awake()
    {   
        _playerInputHandler.MoveEvent += OnMove;
        _playerInputHandler.JumpEvent += OnJump;
        _playerInputHandler.RotateEvent += OnRotate;
    }

    private void OnMove(Vector3 direction)
    {
        _playerMovementController.Move(direction);  
    }

    private void OnJump()
    {
        _playerMovementController.Jump();
    }

    private void OnRotate(Vector2 direction)
    {
        _playerRotateController.Rotate(direction);
    }
}
