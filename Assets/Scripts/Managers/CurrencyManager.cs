using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour, IService
{
    [SerializeField] private TextMeshProUGUI _currencyText;
    [SerializeField] private int _currencyCount;


    private void Awake()
    {
        RegisterService();
        _currencyText.SetText(_currencyCount.ToString());
    }

    public void AddCurrency(int count)
    {
        _currencyCount += count;
        _currencyText.SetText(_currencyCount.ToString());
    }

    public void RemoveCurrency(int count)
    {
        if(HasEnoughCurrency(count))
        {
            _currencyCount -= count;
            _currencyText.SetText(_currencyCount.ToString());
        }
    }

    public bool HasEnoughCurrency(int count)
    {
        bool hasEnoughCurrency = count <= _currencyCount;
        return hasEnoughCurrency;
    }

    private void OnDestroy()
    {
        SL.Unregister(this);
    }

    public void RegisterService()
    {
        SL.Register(this);
    }

    public void UnregisterService()
    {
        SL.Unregister(this);
    }
}
