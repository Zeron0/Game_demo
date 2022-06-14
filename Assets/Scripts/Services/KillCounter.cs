using System.Collections.Generic;
using Character;
using UI;

namespace Services
{
    public class KillCounter
    {
        private KillsCounterView _killsCounterView;
        private int _bots;
        
        public int Kills { get; private set; }
        
        public KillCounter(List<HealthController> bots, KillsCounterView killsCounterView, ProgressBar progressBar)
        {
            _killsCounterView = killsCounterView;
            _bots = bots.Count;

            foreach (var bot in bots)
            {
                bot.OnDeath += () =>
                {
                    Kills += 1;
                    _killsCounterView.UpdateCounter(Kills);
                    progressBar.Fill((float)Kills / _bots);
                };
            }
        }
    }
}