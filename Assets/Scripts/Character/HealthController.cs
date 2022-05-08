using System;
using System.Collections;
using UnityEngine;

namespace Character
{
    public class HealthController : MonoBehaviour, IDamageable
    {
        [SerializeField] private AnimationController _animationController;
        [SerializeField] private MovementController _movementController;

        [SerializeField] private int _initialHP = 3;
        [SerializeField] private float _deathDelay = 2;

        private int _currentHP;
        private bool _alive = true;

        public Transform Transform => transform;
        public int HealthPoints => _currentHP;
        public bool Alive => _alive;

        private void Awake()
        {
            _currentHP = _initialHP;
        }

        public void TakeDamage(int damage)
        {
            _currentHP -= damage;

            if (_alive && _currentHP <= 0)
            {
                OnDeath();
            }
        }

        public void OnDeath()
        {
            _alive = false;
            _movementController.Stop();
            _animationController?.SetAnimation(AnimationType.Death);
            StartCoroutine(StartDeathTimer());
        }

        private IEnumerator StartDeathTimer()
        {
            yield return new WaitForSeconds(_deathDelay);
            Destroy(gameObject);
        }
    }
}
