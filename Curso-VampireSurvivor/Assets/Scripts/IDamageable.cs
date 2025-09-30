
public interface IDamageable
{
    int Health { get; set; }

    public void TakeDamage(int amount);
    public void RecoverDamage (int amount);
}
