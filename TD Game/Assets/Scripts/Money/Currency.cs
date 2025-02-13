using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace TDGame.Money
{
    public class Currency : MonoBehaviour
    {
        public static Currency Instance { get; private set; }

        [SerializeField] private int _currentMoney;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public int AddCurrency(int amount)
        {
            _currentMoney += amount;
            return _currentMoney;
        }
    }   
}

