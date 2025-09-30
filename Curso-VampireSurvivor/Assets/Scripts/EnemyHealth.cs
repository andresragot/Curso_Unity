using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{

    [SerializeField] private int _health = 100;
    [SerializeField] private int _maxHealth = 100;

    [SerializeField] private int damage = 5;

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
