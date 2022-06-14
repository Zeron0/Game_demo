using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _filling;
        [SerializeField] [Min(0.1f)] private float _factor = 1;

        [Range(0, 1)] private float _value;

        private void Update()
        {
            _filling.fillAmount = Mathf.Lerp(_filling.fillAmount, _value, Time.deltaTime * _factor);
        }

        public void Fill(float value)
        {
            _value = value;
        }
    }
}