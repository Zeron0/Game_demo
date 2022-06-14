using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class HealthController : MonoBehaviour, IDamageable
    {
        public event Action OnDeath;
        
        [SerializeField] private List<SkinnedMeshRenderer> _meshRenderers;
        [SerializeField] private AnimationController _animationController;
        [SerializeField] private MovementController _movementController;
        [SerializeField] private Shader _shader;
        [SerializeField] private Material _dissolve;

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
                Die();
            }
        }

        public void Die()
        {
            _alive = false;
            _movementController.Stop();
            _animationController.SetAnimation(AnimationType.Death);
            StartCoroutine(StartDeathTimer());
            OnDeath?.Invoke();
        }

        private IEnumerator StartDeathTimer()
        {
            yield return new WaitForSeconds(_deathDelay);
            
            foreach (var mesh in _meshRenderers)
            {
                mesh.material = _dissolve;
                StartCoroutine(Dissolve(mesh.material));
            }
            
            Destroy(gameObject, _deathDelay);
        }

        private IEnumerator Dissolve(Material material)
        {
            string field = "_TimeValue";
            float time = 0;

            while (time < 1)
            {
                time += Time.deltaTime;
                material.SetFloat(field, time);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
