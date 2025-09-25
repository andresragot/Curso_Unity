using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class Character_Attack : MonoBehaviour
{

    [SerializeField] private GameObject fire;

    [SerializeField] private Transform transform_fire_right;
    [SerializeField] private Transform transform_fire_left;

    [SerializeField] private bool Shooting = false;

    [SerializeField] private float fire_cooldown = 0.3f;
    private float fire_cooldown_aux = 0;

    [SerializeField] private int MAX_BULLETS = 6;
    private int bullets = 0;

    private SpriteRenderer sr;

    [SerializeField] TextMeshProUGUI bulletsText;
    [SerializeField] Slider bulletsSlider;

    private float visualValueSlider;
    private float target = 0;
    [SerializeField] private float smoothSpeed = 8f;

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
        UpdateText(0);

        bulletsSlider.minValue = 0;
        bulletsSlider.maxValue = MAX_BULLETS;

        UpdateSlider(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (fire_cooldown_aux >= fire_cooldown && Shooting && bullets < MAX_BULLETS)
        {
            GameObject bullet = Instantiate(fire, transform_fire_right.position, Quaternion.Euler(0, 0, 270));
            bullets++;

            Fire_Behaviour fb = bullet.GetComponent<Fire_Behaviour>();
            fb.parent = this;

            if (sr.flipX)
            {
                fb.speed *= -1;
                bullet.transform.rotation = Quaternion.Euler(0, 0, 90);
                bullet.transform.position = transform_fire_left.position;
            }
            fire_cooldown_aux = 0;

            UpdateText(bullets);
            UpdateSlider(bullets);
        }

        fire_cooldown_aux += Time.deltaTime;

        visualValueSlider = Mathf.Lerp(visualValueSlider, target, smoothSpeed * Time.deltaTime);
        bulletsSlider.value = visualValueSlider;
    }

    public void BrokenBullet()
    {
        --bullets;
        if (bullets < 0) bullets = 0;
        UpdateText(bullets);
        UpdateSlider(bullets);
    }

    void UpdateText(int bullets)
    {
        bulletsText.text = "Bullets: " + (MAX_BULLETS - bullets);
    }

    void UpdateSlider(int bullets)
    {
        target = MAX_BULLETS - bullets;
    }
}
