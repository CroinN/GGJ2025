using System.Collections.Generic;
using UnityEngine;

public class VendingMachineController : MonoBehaviour
{
    private enum VendingMachineState
    {
        InTrigger,
        InMenu,
        Idle
    }

    [SerializeField] private Renderer _player;
    [SerializeField] private Renderer _bubbleGun;

    [SerializeField] private List<VendingMachine.Drink> _drinks;
    [SerializeField] private VendingMachineTrigger _trigger;
    [SerializeField] private VendingMachine _vendingMachine;

    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Camera _vendingCamera;

    [SerializeField] VendingMachineState _state = VendingMachineState.Idle;

    [SerializeField] int _currentDrink;

    [SerializeField] private List<GameObject> _colas;
    [SerializeField] private List<GameObject> _fantas;
    [SerializeField] private List<GameObject> _sprites;
    [SerializeField] private List<GameObject> _jermuks;

    private VendingMachineAnimationController _animationController;
    private void Start()
    {
        _trigger.onPlayerEnter += OnPlayerEnter;
        _trigger.onPlayerExit += OnPlayerExit;
        SL.Get<UIManager>().DisableVendingText();
        _animationController = GetComponent<VendingMachineAnimationController>();
    }

    private void Update()
    {
        if (_state == VendingMachineState.InTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ChangeState(VendingMachineState.InMenu);
            }
        }
        else if (_state == VendingMachineState.InMenu)
        {
            if (Input.GetKeyDown(KeyCode.A)) ShiftPosition(-1);
            else if (Input.GetKeyDown(KeyCode.D)) ShiftPosition(1);
            else if (Input.GetKeyDown(KeyCode.Return)) _vendingMachine.Purchase(_drinks[_currentDrink], OnPurchaseComplete);
            else if(Input.GetKeyDown(KeyCode.Escape)) ChangeState(VendingMachineState.Idle);
        }
    }

    private void ChangeState(VendingMachineState newState)
    {
        _state = newState;

        if (_state == VendingMachineState.Idle)
        {
            SwitchMainCamera();
            _player.enabled = true;
            _bubbleGun.enabled = true;
            SL.Get<UIManager>().DisableVendingText();
            SL.Get<InputProvider>().EnableInput();
        }
        else if (_state == VendingMachineState.InTrigger)
        {
            SL.Get<UIManager>().EnableVendingText();
        }
        else if (_state == VendingMachineState.InMenu)
        {
            _player.enabled = false;
            _bubbleGun.enabled = false;        
            SwitchVendingMachineCamera();   
            SL.Get<UIManager>().DisableVendingText();
            SL.Get<InputProvider>().DisableInput();
        }
    }

    private void OnPurchaseComplete(bool isSuccessful)
    {
        if (isSuccessful)
        {
            _animationController.animations[_drinks[_currentDrink]].Invoke();
            Debug.Log("PURCHASE IS SUCCESSFUL");
        }
        else
        {
            Debug.Log("PURCHASE IS NOT SUCCESSFUL");
        }
    }


    private void OnPlayerEnter()
    {
        ChangeState(VendingMachineState.InTrigger);
    }

    private void OnPlayerExit()
    {
        ChangeState(VendingMachineState.Idle);
    }

    private void ShiftPosition(int direction)
    {
        _currentDrink = (_currentDrink + direction) % _drinks.Count;
        if(_currentDrink < 0){
            _currentDrink = _drinks.Count-1;
        }

        if (_drinks[_currentDrink] == VendingMachine.Drink.Cola)
        {
            Highlight(_colas);
        }
        else if (_drinks[_currentDrink] == VendingMachine.Drink.Fanta)
        {
            Highlight(_fantas);
        }
        else if (_drinks[_currentDrink] == VendingMachine.Drink.Sprite)
        {
            Highlight(_sprites);
        }
        else if (_drinks[_currentDrink] == VendingMachine.Drink.Jermuk)
        {
            Highlight(_jermuks);
        }
    }

    private void Highlight(List<GameObject> objects)
    {
        
    }
    
    private void SwitchVendingMachineCamera()
    {
        _mainCamera.gameObject.SetActive(false);
        _vendingCamera.gameObject.SetActive(true);
    }
    
    private void SwitchMainCamera()
    {
        _mainCamera.gameObject.SetActive(true);
        _vendingCamera.gameObject.SetActive(false);
    }

}
