using System.Runtime.Serialization.Json;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Fire_Behaviour : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Values")]
    [SerializeField] public float speed = 10f;
    [SerializeField] private float alive_time = 5f;
    [SerializeField] private int damage = 50;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector2 force = Vector2.right * speed;
        rb.AddForce(force, ForceMode2D.Force);
    }

    void Update()
    {
        alive_time -= Time.deltaTime;
        if (alive_time <= 0f)
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        Debug.Log("He desaparecido!");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        IDamageable enemy = collider.gameObject.GetComponent<IDamageable>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    
}
