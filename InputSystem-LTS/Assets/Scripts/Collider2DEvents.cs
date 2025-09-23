using UnityEngine;

public class Collider2DEvents : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log(name + " ha colisionado con Player " + collision.gameObject.name);
        }
        else
        {
            Debug.Log(name + " No ha colisionado con un Player");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(name + " ha salido de la colision con " + collision.name);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(name + " ha intentado colisionar con " + collision.gameObject.name);
    }

}
