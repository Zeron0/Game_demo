using UnityEngine;

namespace Character
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private AnimationType _animationType;
        
        public void SetAnimation(AnimationType type)
        {
            if (_animationType == type)
            {
                return;
            }

            _animator.ResetTrigger(_animationType.ToString());
            _animationType = type;
            _animator.SetTrigger(type.ToString());
        }
    }

    public enum AnimationType
    {
        Idle,
        Walk,
        Run,
        Attack,
        Death
    }
}
