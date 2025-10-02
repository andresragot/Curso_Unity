using UnityEngine;

public class MeleeWeapon : BaseWeapon
{

    [Header("Melee")]
    public float reach = 1.5f;
    public Vector3 offset = default;

    [SerializeField] float time_to_change = 10f;
    float time_to_change_aux = 0;
    bool is_mirrored = false;

    protected override void Fire()
    {
        Vector3 center = transform.position + transform.forward * reach + offset;
        ApplyFalloff(center);
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + transform.forward * reach + offset, 0.05f);
    }

    protected override void Update()
    {
        base.Update();
        if (time_to_change_aux >= time_to_change)
        {
            time_to_change_aux = 0;

            transform.localPosition = new Vector3 (transform.localPosition.x * -1, transform.localPosition.y, transform.localPosition.z);
            if (is_mirrored)
            {
                transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z - 90);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z + 90);
            }

            is_mirrored = !is_mirrored;
        }

        time_to_change_aux += Time.deltaTime;
    }
}
