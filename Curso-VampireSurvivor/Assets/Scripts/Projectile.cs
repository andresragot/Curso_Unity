using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Projectile : MonoBehaviour
{
    ProjectileWeapon weapon;
    GameObject owner;
    float speed;
    float life;

    public void Initalize (ProjectileWeapon weapon, float speed, float life, GameObject owner)
    {
        this.weapon = weapon;
        this.speed = speed;
        this.life = life;
        this.owner = owner;

        CancelInvoke(nameof(Deactivate));
        Invoke(nameof(Deactivate), life);
    }


    private void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    void Deactivate()
    {
        CancelInvoke(nameof(Deactivate));
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Deactivate));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (weapon == null)
        {
            Debug.LogWarning("Projectile: No weapon configured");
            return;
        }

        if (collision.gameObject == owner)
        {
            return;
        }

        var damageable = collision.GetComponent<IDamageable>();
        if (damageable == null) return;

        weapon.ProjectileHit(gameObject, damageable);

        gameObject.SetActive(false);
    }

}
