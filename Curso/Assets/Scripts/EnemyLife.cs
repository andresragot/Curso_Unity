using UnityEngine;

public class EnemyLife: MonoBehaviour, Ilife
{
    [SerializeField] private float _health = 1000;
    [SerializeField] private float _maxHealth = 1000;

    bool is_dead = false;

    public float Health
    {
        get
        {
            return _health;
        }

        set
        {
            if (value <= _maxHealth && 0.0f < value)
            {
                _health = value;
            }
            else if (0.0f >= value)
            {
                _health = 0.0f;
                if (!is_dead)
                {
                    is_dead = true;
                    Destroy(gameObject);
                }
            }
            else if (_maxHealth < value)
            {
                _health = _maxHealth;
            }
        }

    }

    public void TakeDamage(float dmg)
    {
        Health -= dmg;
    }

    public void RecoverDamage(float amount) {}
}
