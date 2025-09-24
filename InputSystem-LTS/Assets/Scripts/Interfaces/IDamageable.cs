public interface IDamageable
{
    int Health { get; set; }
    void TakeDamage(int amount);
    void AddHealth(int amount);
}
