using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private GameObject projectile_prefab;

    [SerializeField] private float cooldown = 0.5f;
    private float cooldown_aux = 0f;

    bool fire = false;

    public void OnFire(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            fire = true;
        }
        else if (ctx.canceled)
        {
            fire = false;
        }
    }

    private void Update()
    {
        cooldown_aux -= Time.deltaTime;
        if (cooldown_aux <= 0.0f)
        {
            if (fire)
            {
                GameObject go = Instantiate(projectile_prefab, transform.position, Quaternion.identity);
                go.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                cooldown_aux = cooldown;
            }
        }
    }
}
