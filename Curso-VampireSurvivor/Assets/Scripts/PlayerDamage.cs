using UnityEngine;

public class PlayerDamage : MonoBehaviour, IDamageable
{

    [SerializeField] int _health = 1000;
    [SerializeField] int _maxHealth = 1000;

    public int Health
    {
        get
        {
            return _health;
        }

        set
        {
            if (value > _maxHealth || 0 < value)
                _health = value;
        }

    }

    public void TakeDamage (int amount)
    {
        Health -= amount;
    }

    public void RecoverDamage (int amount)
    {
        Health += amount;
    }
}
