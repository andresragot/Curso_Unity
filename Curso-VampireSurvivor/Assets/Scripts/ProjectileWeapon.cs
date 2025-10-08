using UnityEngine;

public class ProjectileWeapon : BaseWeapon
{
    public ObjectPool pool;
    public Transform spawnPoint;
    public float projectileSpeed = 10f;
    public float projectileLife = 5f;
    public bool projectileExplodesOnHit = false; // Si es true, usa el falloff

    public Transform enemy;

    protected override void Fire()
    {
        if (spawnPoint == null)
        {
            Debug.LogWarning("ProjectileWeapon: falta spawnpoint");
            return;
        }

        GameObject go = pool.Get(spawnPoint.position, transform.rotation);

        var projectile = go.GetComponent<Projectile>();
        if (projectile == null) go.AddComponent<Projectile>();

        projectile.Initalize(this, projectileSpeed, projectileLife, owner);
    }

    public void ProjectileHit (GameObject go, IDamageable damageable)
    {
        if (projectileExplodesOnHit)
        {
            ApplyFalloff(go.transform.position);
        }
        else
        {
            ApplyDirectDamage(damageable, go.transform.position, go.transform.right);
        }
    }

    public override void Upgrade()
    {
        if (canUpgrade())
        {
            level++;
            baseDamage += level * 5;
            fireRate ++;
        }
    }

    protected override void Update()
    {
        base.Update();
        if (enemy != null)
        {
            Vector3 diff = enemy.position - transform.position;
            float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Enemy detected");
        if (enemy != null) return;

        enemy = collision.transform;
    }
}
