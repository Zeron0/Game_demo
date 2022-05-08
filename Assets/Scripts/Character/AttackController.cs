using System.Collections;
using UnityEngine;

namespace Character
{
    public class AttackController : MonoBehaviour, IDamager
    {
        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private AnimationController _animationController;
        [SerializeField] private Arrow _arrowPrefab;
        [SerializeField] private Transform _arrowSpawnPoint;

        [SerializeField] [Min(1)] private int _damage = 1;
        [SerializeField] [Min(1)] private float _radius = 15f;
        [SerializeField] [Min(0.1f)] private float _attackInterval = 1f;
        [SerializeField] [Min(0.1f)] private float _interval = 0.25f;
        [SerializeField] private LayerMask _mask;

        private Collider[] _hitColliders;
        
        private IDamageable _target;
        private WaitForSeconds _finding;
        private WaitForSeconds _attacking;
        
        private bool _isFinding;
        private bool _canAttack = true;

        public int Damage => _damage;

        private void Awake()
        {
            _hitColliders = new Collider[10];
            _finding = new WaitForSeconds(_interval);
            _attacking = new WaitForSeconds(_attackInterval);
            StartCoroutine(UpdateAsync());
        }

        private void Update()
        {
            if (playerMovementController.IsMoving)
            {
                _isFinding = false;
            }
            else
            {
                _isFinding = true;
            }
        }
        
        public void Hurt(IDamageable damageable)
        {
            damageable.TakeDamage(Damage);
        }

        private IEnumerator UpdateAsync()
        {
            while (true)
            {
                yield return _finding;

                int colliders = Physics.OverlapSphereNonAlloc(transform.position, _radius, _hitColliders, _mask);

                if (!_isFinding || !_canAttack || colliders == 0)
                {
                    continue;
                }

                float minDistance = Mathf.Infinity;
                _target = null;
                    
                for (int i = 0; i < colliders; i++)
                {
                    Vector3 direction = _hitColliders[i].transform.position - _arrowSpawnPoint.position;
                    Ray ray = new Ray(_arrowSpawnPoint.position, direction);
                    RaycastHit hit;
                    Physics.Raycast(ray, out hit, _radius);
                    
                    if (hit.transform != _hitColliders[i].transform)
                    {
                        continue;
                    }

                    float distance = (_hitColliders[i].transform.position - transform.position).magnitude;

                    if (distance < minDistance && hit.collider.TryGetComponent(out IDamageable damageable) && damageable.Alive)
                    {
                        _target = damageable;
                        minDistance = distance;
                    }
                }

                if (_target != null)
                {
                    StartCoroutine(Attack());
                }
            }
        }

        private IEnumerator Attack()
        {
            Hurt(_target);
            playerMovementController.SetTarget(_target.Transform);
            Quaternion rotation = Quaternion.LookRotation(_target.Transform.position - _arrowSpawnPoint.position + Vector3.up);
            Instantiate(_arrowPrefab, _arrowSpawnPoint.position, rotation).Init(_target.Transform);
            _canAttack = false;
            _animationController.SetAnimation(AnimationType.Attack);
            yield return _attacking;
            _canAttack = true;
        }
    }
}