using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Character;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services
{
    public class GameProcessController : MonoBehaviour
    {
        [SerializeField] private TimerHolder _timerHolder;
        [SerializeField] private KillsCounterView _killsCounterView;
        [SerializeField] private ProgressBar _progressBar;
        [SerializeField] private StartWindow _startWindow;
        [SerializeField] private FinalWindow _finalWindow;
        [SerializeField] [Min(0)] private int _needKilling = 5;

        private List<AttackController> _attackControllers = new List<AttackController>();
        private List<HealthController> _healthControllers = new List<HealthController>();
        private KillCounter _killCounter;

        private void Awake()
        {
            CalculateBots();
            _finalWindow.gameObject.SetActive(false);
            _killCounter = new KillCounter(_healthControllers, _killsCounterView, _progressBar);
            _startWindow.OnGameStart += StartGame;
            _timerHolder.OnTimerOver += Complete;
        }

        public void Reload()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
            
            _finalWindow.gameObject.SetActive(true);
            _finalWindow.SetText(gameResult.Won ? "Level completed" : "Level failed");
        }
    }

    public struct GameResult
    {
        public bool Won;
    }
}
