using System;
using System.Collections;
using UnityEngine;

namespace Character
{
    public class Arrow : MonoBehaviour, IDamager
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private float _speed = 6f;
        [SerializeField] private float _maxDistance = 15f;

        private Vector3 _initialPos;

        private bool _isInit;
        
        public int Damage => _damage;

        private void Update()
        {
            transform.position += transform.forward * _speed * Time.deltaTime;

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

        public void Init()
        {
            _isInit = true;
            _initialPos = transform.position;
        }

        public void Hurt(IDamageable damageable)
        {
            damageable.TakeDamage(Damage);
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent(out IDamageable damageable))
            {
                Hurt(damageable);
            }
        }
    }
}
