namespace Character
{
    public interface IDamager
    {
        public int Damage { get; }

        public void Hurt(IDamageable damageable);
    }
}