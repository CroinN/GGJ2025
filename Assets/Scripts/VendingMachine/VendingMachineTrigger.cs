using System;
using UnityEngine;

public class VendingMachineTrigger : MonoBehaviour
{
    [SerializeField] private string _playerTag;
    public Action onPlayerEnter;
    public Action onPlayerExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_playerTag))
        {
            onPlayerEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_playerTag))
        {
            onPlayerExit?.Invoke();
        }
    }
}
