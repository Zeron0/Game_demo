using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace General
{
    public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] private Image _joystick;
        [SerializeField] private Image _container;

        private Vector3 _direction;
        private Vector3 _startPos;

        private float _distance;
        
        public Vector3 Direction => _direction;

        private void Start()
        {
            _startPos = _container.rectTransform.position;
            _distance = _container.rectTransform.sizeDelta.x / 2;
            _container.gameObject.SetActive(false);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 pos = Vector2.zero;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _container.rectTransform, 
                    eventData.position,
                    eventData.pressEventCamera, 
                    out pos))
            {
                pos.x /= _container.rectTransform.sizeDelta.x;
                pos.y /= _container.rectTransform.sizeDelta.y;

                Vector2 p = _container.rectTransform.pivot;
                pos.x += p.x - 0.5f;
                pos.y += p.y - 0.5f;

                float x = Mathf.Clamp(pos.x, -1, 1);
                float y = Mathf.Clamp(pos.y, -1, 1);

                _direction = new Vector3(x, 0, y).normalized;
                _joystick.rectTransform.anchoredPosition = new Vector2(
                    _direction.x * _distance, 
                    _direction.z * _distance);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _direction = default;
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
