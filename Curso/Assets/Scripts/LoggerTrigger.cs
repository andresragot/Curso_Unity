using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class LoggerTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Trigger Enter detected with " + collision.gameObject.name);
        }
        else
        {
            Debug.LogWarning("Trigger not with player");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Trigger Stay detected with " + collision.gameObject.name);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Trigger Exit detected with " + collision.gameObject.name);
    }
}
