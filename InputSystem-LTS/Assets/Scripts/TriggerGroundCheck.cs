using UnityEngine;

public class TriggerGroundCheck : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Suelo");
    }

    void OTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Saltamos!!!!");
    }
}
