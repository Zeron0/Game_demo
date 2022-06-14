using System.Collections.Generic;
using Character;
using UI;
using UnityEngine;

namespace Services
{
    public class KillCounter
    {
        private KillsCounterView _killsCounterView;
        
        public int Kills { get; private set; }
        
        public KillCounter(List<HealthController> bots, KillsCounterView killsCounterView)
        {
            _killsCounterView = killsCounterView;

            foreach (var bot in bots)
            {
                bot.OnDeath += () =>
                {
                    Kills += 1;
                    _killsCounterView.UpdateCounter(Kills);
                };
            }
        }
    }
}