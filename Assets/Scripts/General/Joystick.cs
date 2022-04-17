using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace General
{
    public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Image _joystick;
        [SerializeField] private Image _container;

        private Vector3 _startPos;
        private Vector2 _input;

        private float _distance;
        
        public Vector2 Direction => _input;

        private void Start()
        {
            _startPos = _container.rectTransform.position;
            _distance = _container.rectTransform.sizeDelta.x / 2;
            _container.gameObject.SetActive(false);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 position = RectTransformUtility.WorldToScreenPoint(_canvas.worldCamera, _container.transform.position);
            _input = (eventData.position - position) / (_distance * _canvas.scaleFactor);
                
            if (_input.magnitude > 0)
            {
                if (_input.magnitude > 1)
                {
                    _input = _input.normalized;
                }
            }
            else
            {
                _input = Vector2.zero;
            }

            _joystick.rectTransform.anchoredPosition = _input * _distance;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _input = default;
            _joystick.rectTransform.anchoredPosition = default;
            _container.rectTransform.position = _startPos;
            _container.gameObject.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _container.rectTransform.position = eventData.position;
            _container.gameObject.SetActive(true);
            OnDrag(eventData);
        }
    }
}
