using System;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private Dictionary<AnimationType, string> _animations = new Dictionary<AnimationType, string>();

        private AnimationType _animationType;

        private void Awake()
        {
            _animations.Add(AnimationType.Idle, AnimationType.Idle.ToString());
            _animations.Add(AnimationType.Walk, AnimationType.Walk.ToString());
            _animations.Add(AnimationType.Run, AnimationType.Run.ToString());
            _animations.Add(AnimationType.Attack, AnimationType.Attack.ToString());
            _animations.Add(AnimationType.Death, AnimationType.Death.ToString());
        }

        public void SetAnimation(AnimationType type)
        {
            if (_animationType == type)
            {
                return;
            }

            _animator.ResetTrigger(_animations[_animationType]);
            _animationType = type;
            _animator.SetTrigger(_animations[type]);
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
