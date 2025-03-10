using System;
using UnityEngine;

namespace TDGame.Money
{
    public class Currency : MonoBehaviour
    {
        public event Action<int> OnCurrencyChanged;
        [SerializeField] private int _currentMoney = 20;
        public int CurrentMoney => _currentMoney;

        public void AddCurrency(int amount)
        {
            _currentMoney += amount;
            OnCurrencyChanged?.Invoke(_currentMoney);
        }
    }   
}

