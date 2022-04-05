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
    
        private void Update()
        {
            if (_joystick.Direction == Vector3.zero)
            {
                _animationController.SetAnimation(AnimationType.Idle);
                return;
            }

            transform.rotation = Quaternion.LookRotation(_joystick.Direction);
            _animationController.SetAnimation(AnimationType.Run);
            _characterController.SimpleMove(_joystick.Direction * _speed);
        }
    }
}
