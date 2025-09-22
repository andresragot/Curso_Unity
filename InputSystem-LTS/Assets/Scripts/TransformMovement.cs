using UnityEngine;

public class TransformMovement : MonoBehaviour
{
    public float speed = 5f;
    public Vector2 position_to_look;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // double example = 0.5;
        // float example_f = 0.5f;
        // transform.localPosition = new Vector3(1, 1, 0);
        // transform.rotation = Quaternion.Euler(0, 0, 45);
        // transform.localScale = new Vector3(1f, 1f, 1f);

    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 VectorRight = new Vector3(1, 0, 0);

        // Mover hacia la izquierda
        // transform.rotation = Quaternion.Euler(0, 0, 0);
        // transform.Translate(Vector3.right * -0.02f);
        // transform.rotation = Quaternion.Euler(0, 0, 45);¡

        // Look At con Unity (3D)
        // Vector3 target = new Vector3(position_to_look.x, position_to_look.y, transform.position.z);
        // transform.LookAt(target, Vector3.up);

        // LookAt a mano para 2D
        // Vector3 target = new Vector3(position_to_look.x, position_to_look.y, 0);
        // Vector3 dir = target - transform.position;

        // float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }
}
