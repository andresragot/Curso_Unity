using UnityEngine;
using UnityEngine.InputSystem;

public class NewInput_Example : MonoBehaviour
{
    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            string key = "Unknown";
            key = ctx.control.displayName;

            Debug.Log("[DOWN] " + key);
        }
        else if (ctx.canceled)
        {
            string key = "Unknown";
            key = ctx.control.displayName;

            Debug.Log("[UP] " + key);
        }

        Vector2 dir = ctx.ReadValue<Vector2>();
        Debug.Log("Vector: " + dir);
    }
}
