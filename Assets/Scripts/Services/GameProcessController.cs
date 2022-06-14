using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Character;
using UI;
using UnityEngine;

namespace Services
{
    public class GameProcessController : MonoBehaviour
    {
        [SerializeField] private TimerHolder _timerHolder;
        [SerializeField] private KillsCounterView _killsCounterView;
        [SerializeField] private StartWindow _startWindow;
        [SerializeField] [Min(0)] private int _needKilling = 5;

        private List<AttackController> _attackControllers = new List<AttackController>();
        private List<HealthController> _healthControllers = new List<HealthController>();
        private KillCounter _killCounter;

        private void Awake()
        {
            CalculateBots();
            _killCounter = new KillCounter(_healthControllers, _killsCounterView);
            _startWindow.OnGameStart += StartGame;
            _timerHolder.OnTimerOver += Complete;
        }

        private void StartGame()
        {
            _timerHolder.Init();

            foreach (var attackController in _attackControllers)
            {
                attackController.Init();
            }
        }

        private void CalculateBots()
        {
            _attackControllers = GetComponentsInChildren<AttackController>().ToList();
            _healthControllers = GetComponentsInChildren<HealthController>().ToList();
        }

        private void Complete()
        {
            var gameResult = new GameResult()
            {
                Won = _killCounter.Kills >= _needKilling
            };
        }
    }

    public struct GameResult
    {
        public bool Won;
    }
}
