using System.Collections;
using Character;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace AI
{
    public class BotMovementHandler : MovementController
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] [Min(1)] private float _actionInterval = 1f;
        [SerializeField] [Min(1)] private float _range = 3f;

        private WaitForSeconds _interval;
        private Vector3 _initialPosition;
        private Vector3 _targetPosition;
        private bool _isFindingPath;
        
        private void Awake()
        {
            _interval = new WaitForSeconds(_actionInterval);
            _initialPosition = transform.position;
        }

        private void Update()
        {
            if (!_canMove)
            {
                return;
            }
            
            if (!_agent.hasPath)
            {
                if (_isFindingPath)
                {
                    return;
                }

                StartCoroutine(FindTargetPosition());
            }
        }

        private IEnumerator FindTargetPosition()
        {
            _isFindingPath = true;
            _animationController?.SetAnimation(AnimationType.Idle);
            yield return _interval;
            _isFindingPath = false;
            _animationController?.SetAnimation(AnimationType.Walk);
            _targetPosition = _initialPosition + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f) * _range);
            _agent.SetDestination(_targetPosition);
        }

        public override void Stop()
        {
            _canMove = false;
            _agent.enabled = false;
            StopAllCoroutines();
        }
    }
}
