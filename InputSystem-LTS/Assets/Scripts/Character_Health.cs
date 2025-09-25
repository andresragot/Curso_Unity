using UnityEngine;
using UnityEngine.UI;

public class Character_Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = 6;
    [SerializeField] private int max_health = 8;

    [SerializeField] Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    void Start()
    {
        UpdateHealth();
    }

    public int Health { get => health; set { if (value <= max_health && value >= 0) health = value; } }

    void UpdateHealth()
    {
        for (int i = 0; i < hearts.Length; ++i)
        {
            int heartHealth = Health - (i * 2);

            if (heartHealth >= 2)
            {
                hearts[i].sprite = fullHeart;
            }
            else if (heartHealth == 1)
            {
                hearts[i].sprite = halfHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;

        Debug.Log("Damage taken, rest: " + Health);

        UpdateHealth();
        if (Health <= 0)
        {
            // YOU DIED!
            Destroy(gameObject);
        }
    }

    public void AddHealth(int amount)
    {
        Health += amount;

        UpdateHealth();
    }
    

}
