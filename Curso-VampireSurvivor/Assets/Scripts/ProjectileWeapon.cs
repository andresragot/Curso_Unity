using UnityEngine;

public class ProjectileWeapon : BaseWeapon
{
    public ObjectPool pool;
    public Transform spawnPoint;
    public float projectileSpeed = 10f;
    public float projectileLife = 5f;
    public bool projectileExplodesOnHit = false; // Si es true, usa el falloff

    protected override void Fire()
    {
        if (spawnPoint == null)
        {
            Debug.LogWarning("ProjectileWeapon: falta spawnpoint");
            return;
        }

        GameObject go = pool.Get(spawnPoint.position, Quaternion.identity);

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
}
