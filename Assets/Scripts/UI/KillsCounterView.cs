using TMPro;
using UnityEngine;

namespace UI
{
    public class KillsCounterView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void UpdateCounter(int kills)
        {
            _text.text = $"Kills: {kills}";
        }
    }
}
