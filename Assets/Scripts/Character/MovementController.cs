using UnityEngine;

namespace Character
{
    public abstract class MovementController : MonoBehaviour
    {
        [SerializeField] protected AnimationController _animationController;
        protected bool _canMove = true;
        public abstract void Stop();
    }
}