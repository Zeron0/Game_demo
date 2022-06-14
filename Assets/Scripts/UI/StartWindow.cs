using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class StartWindow : MonoBehaviour, IPointerDownHandler
    {
        public event Action OnGameStart;

        [SerializeField] private TextMeshProUGUI _text;
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetTaskText(string text)
        {
            _text.text = text;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnGameStart?.Invoke();
            Hide();
        }
    }
}
