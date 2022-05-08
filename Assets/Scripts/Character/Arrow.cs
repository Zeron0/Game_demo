using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Character
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private float _speed = 6f;
        [SerializeField] private float _maxDistance = 15f;

        private Vector3 _initialPos;
        private Transform _target;

        private bool _isInit;
        
        public int Damage => _damage;

        private void Update()
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            transform.position += direction * _speed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(direction);

            if (_isInit == false)
            {
                return;
            }

            float distance = transform.position.magnitude - _initialPos.magnitude;

            if (distance > _maxDistance)
            {
                Destroy(gameObject);
            }
        }

        public void Init(Transform target)
        {
            _isInit = true;
            _target = target;
            _initialPos = transform.position;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject);
        }
    }
}
