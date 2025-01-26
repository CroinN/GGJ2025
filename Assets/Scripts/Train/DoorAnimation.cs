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

    public void OpenDoors(Action callback = null)
    {
        Sequence sequence = DOTween.Sequence();
        
        foreach (Door door in _doors)
        {
            sequence.Join(door.left.DOMove(door.left.position + door.left.transform.forward, 1)
                .SetEase(Ease.Linear));
            sequence.Join(door.right.DOMove(door.right.position - door.left.transform.forward, 1)
                .SetEase(Ease.Linear));
        }
        
        sequence.OnComplete(()=>callback?.Invoke());
    }
    
    public void CloseDoors(Action callback = null)
    {
        Sequence sequence = DOTween.Sequence();
        foreach (Door door in _doors)
        {
            sequence.Join(door.left.DOMove(door.left.position - door.left.transform.forward, 1)
                .SetEase(Ease.Linear));
            sequence.Join(door.right.DOMove(door.right.position + door.left.transform.forward, 1)
                .SetEase(Ease.Linear));
        }

        sequence.OnComplete(()=>callback?.Invoke());
    }
}
