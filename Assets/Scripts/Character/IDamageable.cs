using UnityEngine;

namespace Character
{
    public interface IDamageable
    {
        public Transform Transform { get; }
        public int HealthPoints { get; }
        public bool Alive { get; }

        public void TakeDamage(int damage);
        public void OnDeath();
    }
}
