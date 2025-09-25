using UnityEngine;

public class Enemy_Behaviour : MonoBehaviour, IDamageable
{

    [SerializeField] private int health = 100;
    [SerializeField] private int damage = 100;

    public int Health
    {
        get { return health; }

        set
        {
            health = value;
        }
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        Debug.Log("Recibi " + amount + " de daño. Vida restante: " + Health);

        if (Health <= 0)
        {
            Debug.LogError("Enemigo muerto");
            Destroy(gameObject);
        }
    }

    public void AddHealth(int amount) { }

    void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable player = collision.gameObject.GetComponent<IDamageable>();

        if (player != null)
        {
            Debug.Log("Damaging player");
            player.TakeDamage(damage);
        }
    }

}
