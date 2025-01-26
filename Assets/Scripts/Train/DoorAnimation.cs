using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    [Serializable]
    private struct Door
    {
        public Transform left;
        public Transform right;
    }
    [SerializeField] private List<Door> _doors;

    public void OpenDoors()
    {
        foreach (Door door in _doors)
        {
            door.left.DOMove(door.left.position + transform.right, 1)
                .SetEase(Ease.Linear);
            door.right.DOMove(door.right.position - transform.right, 1)
                .SetEase(Ease.Linear);
        }
    }
    
    public void CloseDoors()
    {
        foreach (Door door in _doors)
        {
            door.left.DOMove(door.left.position - transform.right, 1)
                .SetEase(Ease.Linear);
            door.right.DOMove(door.right.position + transform.right, 1)
                .SetEase(Ease.Linear);
        }
    }

    private void Start()
    {
        OpenDoors();
        DOVirtual.DelayedCall(5, CloseDoors);
    }
}
