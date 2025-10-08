using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{

    [SerializeField] private float _health = 100f;
    [SerializeField] private float _maxHealth = 100f;

    [SerializeField] private float damage = 5;
    [SerializeField] private int xp = 10;

    public LevelSystem levelSystem;

    private Poolable poolable;

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

        if (Health <= 0.0f)
        {
            if (poolable != null) poolable.Despawn();
            else gameObject.SetActive(false);
        }
    }

    public void RecoverDamage (float amount)
    {
        Health += amount;
    }

    void Awake()
    {
        poolable = GetComponent<Poolable>();
    }

    void OnEnable()
    {
        RecoverDamage(_maxHealth);
    }

    void OnDisable()
    {
        levelSystem?.AddXP(xp);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                Damage dmg = new Damage (damage);
                damageable.TakeDamage(dmg);
            }
        }
    }
}
