using UnityEngine;

public class Character_Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = 100;
    [SerializeField] private int max_health = 500;

    public int Health { get => health; set {if (value <= max_health && value >= 0) health = value;}  }

    public void TakeDamage(int amount)
    {
        Health -= amount;

        if (Health <= 0)
        {

            // YOU DIED!
            Destroy(gameObject);
        }
    }

    public void AddHealth(int amount)
    {
        Health += amount;
    }
}
