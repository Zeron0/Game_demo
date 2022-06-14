using TMPro;
using UnityEngine;
namespace UI
{
    public class FinalWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void SetText(string text)
        {
            _text.SetText(text);
        }
    }
}
