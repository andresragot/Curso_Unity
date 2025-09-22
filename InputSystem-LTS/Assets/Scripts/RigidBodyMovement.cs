using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RigidBodyMovement : MonoBehaviour
{
    public float speed = 5f;
    public Vector2 moveInput;
    private Rigidbody2D rb;

    // Sucede antes de comenzar la escena
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Obtener el tiempo de diferencia entre el frame anterior y este.
        // Time.deltaTime;
    }

    // Este es el update específico de las físicas
    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * speed;
    }
}
