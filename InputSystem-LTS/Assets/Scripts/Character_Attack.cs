using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

[RequireComponent(typeof(SpriteRenderer))]
public class Character_Attack : MonoBehaviour
{

    [SerializeField] private GameObject fire;

    [SerializeField] private Transform transform_fire_right;
    [SerializeField] private Transform transform_fire_left;

    [SerializeField] private bool Shooting = false;

    [SerializeField] private float fire_cooldown = 0.3f;
    private float fire_cooldown_aux = 0;

    private SpriteRenderer sr;

    public void OnFire(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            Shooting = true;
        }
        else if (ctx.canceled)
        {
            Shooting = false;
        }

    }

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (fire_cooldown_aux >= fire_cooldown && Shooting)
        {
            GameObject bullet = Instantiate(fire, transform_fire_right.position, Quaternion.Euler(0, 0, 270));
            if (sr.flipX)
            {
                Fire_Behaviour fb = bullet.GetComponent<Fire_Behaviour>();
                fb.speed *= -1;
                bullet.transform.rotation = Quaternion.Euler(0, 0, 90);
                bullet.transform.position = transform_fire_left.position;
            }
                fire_cooldown_aux = 0;
        }

        fire_cooldown_aux += Time.deltaTime;
    }
}
