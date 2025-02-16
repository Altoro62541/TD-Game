using TDGame.Money;
using TMPro;
using UnityEngine;
using Zenject;

namespace TDGame.UI
{
    public class CurrencyUI : MonoBehaviour
    {
    [SerializeField] private TextMeshProUGUI _currencyText;

    private Currency _currency;

    [Inject]
    public void Construct(Currency currency)
    {
        _currency = currency;
    }

    private void OnEnable()
    {
        if (_currency != null)
        {
            _currency.OnCurrencyChanged += UpdateCurrency;
            UpdateCurrency(_currency.CurrentMoney);
        }
    }

    private void OnDisable()
    {
        if (_currency != null)
            _currency.OnCurrencyChanged -= UpdateCurrency;
    }

    public void UpdateCurrency(int currentMoney)
    {
        if (_currencyText == null)
        {
            Debug.LogError("CurrencyText не привязан!");
            return;
        }
        _currencyText.text = $"Money: {currentMoney}";
    }
    }

}


