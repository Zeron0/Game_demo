using System;
using System.Collections.Generic;
using Character;
using Unity.Mathematics;
using UnityEngine;

namespace AI
{
    public class RouteHandler : MovementController
    {
        [SerializeField] private List<Transform> _points;
        [SerializeField] [Min(0.1f)] private float _speed = 2f;

        private Vector3 _averagePosition;
        private int _pointIndex = 1;
        private float _t;
        private bool _isReturning;
        private bool _isStarted;

        private void Start()
        {
            StartMove();
        }

        private void Update()
        {
            if (!_canMove)
            {
                return;
            }
            
            _t = Mathf.Clamp01(_t + Time.deltaTime * _speed);

            if (_t == 1)
            {
                GoToNextPoint();
                CalculateAveragePoint();
            }

            Vector3 nextPosition = GetPositionOnBezierCurve(transform.position, _averagePosition, _points[_pointIndex].position, _t);
            Quaternion rotation = Quaternion.LookRotation(nextPosition - transform.position);

            if (rotation != quaternion.identity)
            {
                transform.rotation = rotation;
            }
            
            transform.position = nextPosition;
        }

        private void StartMove()
        {
            transform.position = _points[0].position;
            CalculateAveragePoint();
        }

        private void GoToNextPoint()
        {
            _t = 0;

            if (_isReturning && _pointIndex > 0)
            {
                _pointIndex -= 1;
            }
            else if (_pointIndex < _points.Count - 1)
            {
                _pointIndex += 1;
            }
            
            if (_pointIndex == 0 || _pointIndex == _points.Count - 1)
            {
                _isReturning = !_isReturning;
            }
        }

        private void CalculateAveragePoint()
        {
            _averagePosition = transform.position + transform.forward * ((_points[_pointIndex].position - transform.position).magnitude / 2);
        }

        private Vector3 GetPositionOnBezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            return (1 - t) * ((1 - t) * p0 + t * p1) + t * ((1 - t) * p1 + t * p2);
        }

        public override void Stop()
        {
            _canMove = false;
        }
    }
}
