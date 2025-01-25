using System;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachineUIController : MonoBehaviour
{
    [SerializeField] private List<VendingMachine.Drink> _drinks;
    [SerializeField] private VendingMachineTrigger _trigger;
    [SerializeField] private VendingMachine _vendingMachine;

    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Camera _vendingCamera;

    private int _currentDrink;
    private bool _waitingForInput = false;
    private bool _enteredMenu = false;
    private void Start()
    {
        _trigger.onPlayerEnter += OnPlayerEnter;
        _trigger.onPlayerExit += OnPlayerExit;
    }

    private void HandleMenuInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitMenu();
            _enteredMenu = false;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            ShiftPosition(-1);
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            ShiftPosition(1);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            _vendingMachine.Purchase(_drinks[_currentDrink], PurchaseCallback);
        }
        
    }
    
    private void Update()
    {
        if (_waitingForInput && !_enteredMenu && Input.GetKeyDown(KeyCode.E))
        {
            EnterMenu();
        }
        else if (_waitingForInput && _enteredMenu)
        {
            HandleMenuInput();
        }
    }

    private void PurchaseCallback(bool isSuccessful)
    {
        if (isSuccessful)
        {
            Debug.Log("You bought " + _drinks[_currentDrink]);
        }
        else
        {
            Debug.Log("Insufficient funds!");
        }
    }

    private void EnterMenu()
    {
        SwitchVendingMachineCamera();
    }
    private void ExitMenu()
    {
        SwitchMainCamera();
    }
    
    private void OnPlayerEnter()
    {
        SL.Get<UIManager>().EnableVendingText();
        _waitingForInput = true;
    }

    private void OnPlayerExit()
    {
        SL.Get<UIManager>().DisableVendingText();
        _waitingForInput = false;
    }

    private void ShiftPosition(int direction)
    {
        _currentDrink = Mathf.Clamp((_currentDrink + direction) % _drinks.Count, 0, _drinks.Count);
        
        _vendingMachine.SelectDrink(_drinks[_currentDrink]);
    }
    
    private void SwitchVendingMachineCamera()
    {
        _mainCamera.gameObject.SetActive(false);
        _vendingCamera.gameObject.SetActive(true);
    }
    
    private void SwitchMainCamera()
    {
        _mainCamera.gameObject.SetActive(false);
        _vendingCamera.gameObject.SetActive(true);
    }

}
