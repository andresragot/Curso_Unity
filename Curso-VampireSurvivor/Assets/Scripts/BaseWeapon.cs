using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    [Header("Basics")]
    public float baseDamage = 10;
    public float fireRate = 1f;
    public GameObject owner;
    public string weaponName = "";

    [Header("Falloff")]
    [Tooltip("Radio del falloff")]
    public float falloffRadius = 2f;
    [Tooltip("Si true, daño cae con la distancia dentro del radio")]
    public bool useFalloff = true;
    [Tooltip("Exponente de caida")]
    public float falloffExponent = 1f;

    [Header("Detección")]
    public LayerMask damageableLayers;

    [Header("Levels")]
    public int level = 1;
    public int maxLevel = 3;

    protected float lastFireTime = -999f;
    protected float cooldown => 1f / Mathf.Max(0.00001f, fireRate);

    // C/C++ ->
    // JS [] => {}
    // C# => fda;

    protected virtual void Update()
    {
        TryFire();
    }

    public bool TryFire()
    {
        if (Time.time - lastFireTime < cooldown) return false;

        lastFireTime = Time.time;
        Fire ();
        return true;
    }

    protected abstract void Fire();

    // Privates -> Nadie ademas de la clase puede acceder a ellas
    // Publics -> Todos pueden acceder a ellas
    // Protected -> Otras clases no pueden acceder a ella, pero sus hijos si

    protected void ApplyDirectDamage (IDamageable target, Vector3 hitPos, Vector3 dir)
    {
        if (target == null) return;

        var dmg = new Damage (baseDamage, owner, hitPos, dir);
        target.TakeDamage(dmg);
    }


    // Normal
    // ref -> Puede ser que cambie el valor o no
    // out -> Obligatorio de que esta tiene que cambiar

    protected void ApplyFalloff (Vector3 center)
    {
        if (!useFalloff) return;

        var cols = Physics2D.OverlapCircleAll(center, falloffRadius, damageableLayers);

        for (int i = 0; i < cols.Length; ++i)
        {
            var col = cols[i];
            if (col == null) continue;

            if (col.TryGetComponent<IDamageable>(out var damageable) == false)
            {
                var parent = col.GetComponentInParent<IDamageable>();
                if (parent != null) damageable = parent;
            }

            if (damageable == null) continue;

            float finalDamage = baseDamage;

            float dist = Vector3.Distance(center, col.ClosestPoint(center));
            float t = Mathf.Clamp01(dist / Mathf.Max(0.00001f, falloffRadius));
            float multiplier = 1f - Mathf.Pow(t, falloffExponent);
            finalDamage *= multiplier;

            var dmg = new Damage(finalDamage, owner, col.ClosestPoint(center), (col.transform.position - center).normalized);
            damageable.TakeDamage(dmg);
        }
    }

    protected virtual void OnDrawGizmosSelected()
    {
        if (useFalloff)
        {
            Gizmos.color = new Color(1f, 0.2f, 0.2f, 0.3f);
            Gizmos.DrawWireSphere(transform.position, falloffRadius);
        }
    }

    protected bool canUpgrade()
    {
        return (level + 1 <= maxLevel);
    }

    public virtual void Upgrade()
    {
        if (canUpgrade())
        {
            level++;
            Debug.Log ("Se ha subido al nivel: " + level);
        }
        else
        {
            Debug.Log ("Ya está al maximo");
        }
    }
}
