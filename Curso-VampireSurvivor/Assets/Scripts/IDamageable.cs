
public interface IDamageable
{
    float Health { get; set; }

    public void TakeDamage(Damage dmg);
    public void RecoverDamage (float amount);
}
