using General;
using UnityEditor.Animations;
using UnityEngine;

namespace Character
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private AnimationController _animationController;
        [SerializeField] private CharacterController _characterController;

        [SerializeField] private float _speed = 1;
        [SerializeField] private float _attackRotationDegree = 75f;

        private Transform _target;
        
        public bool IsMoving { get; private set; }
        
        private void Update()
        {
            if (_joystick.Direction == Vector3.zero)
            {
                IsMoving = false;

                if (_target)
                {
                    transform.rotation = Quaternion.LookRotation(_target.position - transform.position) * Quaternion.Euler(0, _attackRotationDegree, 0);
                }
                
                _animationController.SetAnimation(AnimationType.Idle);
                
                return;
            }

            IsMoving = true;
            transform.rotation = Quaternion.LookRotation(_joystick.Direction);
            _animationController.SetAnimation(AnimationType.Run);
            _characterController.SimpleMove(_joystick.Direction * _speed);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}
