
public interface Ilife
{
    float Health { get; set; }

    public void TakeDamage(float dmg);
    public void RecoverDamage(float amount);
}
