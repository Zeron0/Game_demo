using General;
using UnityEngine;

namespace Character
{
    public class PlayerMovementController : MovementController
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private CharacterController _characterController;

        [SerializeField] private float _speed = 1;
        [SerializeField] private float _attackRotationDegree = 75f;

        private Transform _target;

        public bool IsMoving { get; private set; }
        
        private void Update()
        {
            if (!_canMove)
            {
                return;
            }
            
            if (_joystick.Direction == Vector2.zero)
            {
                IsMoving = false;
                _animationController.SetAnimation(AnimationType.Idle);

                if (_target)
                {
                    Vector3 targetPosition = new Vector3(_target.position.x, transform.position.y, _target.position.z); 
                    transform.rotation = Quaternion.LookRotation(targetPosition - transform.position) * Quaternion.Euler(0, _attackRotationDegree, 0);
                }

                return;
            }
            
            IsMoving = true;
            Vector3 direction = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);
            transform.rotation = Quaternion.LookRotation(direction);
            _animationController.SetAnimation(AnimationType.Run);
            _characterController.SimpleMove(direction * _speed);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public override void Stop()
        {
            _canMove = false;
        }
    }
}
