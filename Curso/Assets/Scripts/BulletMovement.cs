using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletMovement : MonoBehaviour
{
    public float movementSpeed = 10f;

    public float life_time = 5f;

    public float damage = 100f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Invoke(nameof(Deactivate), life_time);
    }

    private void Deactivate()
    {
        Destroy(gameObject);
    }

    //private void Update()
    //{
    //    life_time -= Time.deltaTime;
    //    if (life_time <= 0.0f)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    private void LateUpdate()
    {
        rb.linearVelocityX = movementSpeed;
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        Debug.Log ("Bullet collisioned with " + collision.gameObject.name);

        Ilife life = collision.gameObject.GetComponent<Ilife>();

        if (life != null)
        {
            life.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
