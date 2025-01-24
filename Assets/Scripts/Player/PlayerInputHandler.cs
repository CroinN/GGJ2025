using System;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public event Action<Vector3> MoveEvent;
    
    private InputProvider _inputProvider;

    private void Start()
    {
        _inputProvider = SL.Get<InputProvider>();
        _inputProvider.MoveEvent += OnMove;
    }

    private void OnMove(Vector3 direction)
    {
        MoveEvent?.Invoke(direction);
    }
}
