using System;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachineAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public Dictionary<VendingMachine.Drink, Action> animations;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        animations = new Dictionary<VendingMachine.Drink, Action>();
        animations.Add(VendingMachine.Drink.Cola, RunColaAnimation);
        animations.Add(VendingMachine.Drink.Fanta, RunFantaAnimation);
        animations.Add(VendingMachine.Drink.Jermuk, RunJermukAnimation);
        animations.Add(VendingMachine.Drink.Sprite, RunSpriteAnimation);
    }

    public void RunFantaAnimation()
    {
        SetAllFalse();
        _animator.SetTrigger("IsFantaSelected");
    }

    public void RunColaAnimation()
    {
        SetAllFalse();
        _animator.SetTrigger("IsColaSelected");
    }

    public void RunJermukAnimation()
    {
        SetAllFalse();
        _animator.SetTrigger("IsJermukSelected");
    }

    public void RunSpriteAnimation()
    {
        SetAllFalse();
        _animator.SetTrigger("IsSpriteSelected");
    }

    private void SetAllFalse()
    {
        _animator.ResetTrigger("IsFantaSelected");
        _animator.ResetTrigger("IsColaSelected");
        _animator.ResetTrigger("IsJermukSelected");
        _animator.ResetTrigger("IsSpriteSelected");
    }
}
