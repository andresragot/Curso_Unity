using UnityEngine;

public class PlayerDamage : MonoBehaviour, IDamageable
{

    [SerializeField] float _health = 1000;
    [SerializeField] float _maxHealth = 1000;

    public float Health
    {
        get
        {
            return _health;
        }

        set
        {
            if (value <= _maxHealth || 0.0f <= value)
                _health = value;
            else if (0.0f > value)
                _health = 0.0f;
            else if (_maxHealth < value)
                _health = _maxHealth;
        }

    }

    public void TakeDamage (Damage dmg)
    {
        Health -= dmg.amount;
    }

    public void RecoverDamage (float amount)
    {
        Health += amount;
    }
}
