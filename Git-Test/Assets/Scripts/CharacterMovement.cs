using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Teclas WASD
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("Presionando W");
        }

        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Presionando A");
        }

        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("Presionando S");
        }

        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Presionanod D");
        }

        // if (Input.GetKey(KeyCode.Mouse0))
        // {
        //     Debug.Log("Presionando Mouse Left");
        // }

        // // ---- Posicion del Ratón
        // Vector3 posRaton = Input.mousePosition; // en pixeles
        // Debug.Log("Posición del ratón: " + posRaton);


        // Array
        // [{},{},{}]

        // ------- Input de Móvil
        if (Input.touchCount > 0)
        {
            Touch toque = Input.GetTouch(0);

            if (toque.phase == TouchPhase.Began)
            {
                Debug.Log("Toque iniciado en: " + toque.position);
            }
            else if (toque.phase == TouchPhase.Moved)
            {
                Debug.Log("Moviendo en: " + toque.position);
            }
            else if (toque.phase == TouchPhase.Ended)
            {
                Debug.Log("Toque terminó en: " + toque.position);
            }
        }

    }
}
