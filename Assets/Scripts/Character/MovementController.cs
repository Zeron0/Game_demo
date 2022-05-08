using UnityEngine;

namespace Character
{
    public abstract class MovementController : MonoBehaviour
    {
        protected bool _canMove = true;
        public abstract void Stop();
    }
}