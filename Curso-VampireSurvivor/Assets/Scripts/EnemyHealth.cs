using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{

    [SerializeField] private int _health = 100;
    [SerializeField] private int _maxHealth = 100;

    [SerializeField] private int damage = 5;

    private Poolable poolable;

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

        if (Health <= 0)
        {
            if (poolable != null) poolable.Despawn();
            else gameObject.SetActive(false);
        }
    }

    public void RecoverDamage (int amount)
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }
    }
}
