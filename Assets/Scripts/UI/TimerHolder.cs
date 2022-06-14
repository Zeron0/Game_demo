using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TimerHolder : MonoBehaviour
    {
        public event Action OnTimerOver;
        
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private int _startValue = 60;

        private float _value;
        private bool _isOver;
        private bool _isInit;
        
        public void Init()
        {
            _isInit = true;
            _value = _startValue;
        }

        private void Update()
        {
            if (!_isInit || _isOver)
            {
                return;
            }
            
            _value -= Time.deltaTime;
            
            if (_value <= 0)
            {
                _value = 0;
                _isOver = true;
                OnTimerOver?.Invoke();
            }

            _text.text = $"Left time: {(int)_value}";
        }
    }
}