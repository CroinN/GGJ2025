using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class VendingMachineController : MonoBehaviour
{
    private enum VendingMachineState
    {
        InTrigger,
        InMenu,
        Idle
    }

    [SerializeField] private TMPro.TMP_Text _priceText;
    [SerializeField] private TMPro.TMP_Text _nameText;
    
    [SerializeField] private Renderer _player;
    [SerializeField] private Renderer _bubbleGun;

    [SerializeField] private List<VendingMachine.Drink> _drinks;
    [SerializeField] private VendingMachineTrigger _trigger;
    [SerializeField] private VendingMachine _vendingMachine;

    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Camera _vendingCamera;

    [SerializeField] VendingMachineState _state = VendingMachineState.Idle;

    [FormerlySerializedAs("_currentDrink")] [SerializeField] int _currentDrinkIndex;

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
            else if (Input.GetKeyDown(KeyCode.Return)) _vendingMachine.Purchase(_drinks[_currentDrinkIndex], OnPurchaseComplete);
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
            _animationController.animations[_drinks[_currentDrinkIndex]].Invoke();
            SL.Get<InventoryManager>().AddItem(_drinks[_currentDrinkIndex]);
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
        _currentDrinkIndex = (_currentDrinkIndex + direction) % _drinks.Count;
        VendingMachine.Drink currentDrink = _drinks[_currentDrinkIndex];
        if(_currentDrinkIndex < 0){
            _currentDrinkIndex = _drinks.Count-1;
        }

        if (currentDrink == VendingMachine.Drink.Cola)
        {
            Highlight(_colas);
            _nameText.SetText("Cola");
        }
        else if (currentDrink == VendingMachine.Drink.Fanta)
        {
            Highlight(_fantas);
            _nameText.SetText("Fanta");
        }
        else if (currentDrink == VendingMachine.Drink.Sprite)
        {
            Highlight(_sprites);
            _nameText.SetText("Sprite");
        }
        else if (currentDrink == VendingMachine.Drink.Jermuk)
        {
            Highlight(_jermuks);
            _nameText.SetText("Jermuk");
        }
        _priceText.SetText("Price: " + _vendingMachine.GetPrice(currentDrink));
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
