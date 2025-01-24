using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInputHandler _playerInputHandler;
    [SerializeField] PlayerMovementController _playerMovementController;

    private void Awake()
    {   
        _playerInputHandler.MoveEvent += OnMove;
    }

    private void OnMove(Vector3 direction)
    {
        _playerMovementController.Move(direction);  
    }
}
