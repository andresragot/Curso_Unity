using UnityEngine;

public class Poolable : MonoBehaviour
{
    private ObjectPool owner;

    private static readonly int DisabledFrame = -1;
    private int lastDisableFrame = DisabledFrame;

    public void SetOwnerPool(ObjectPool pool)
    {
        owner = pool;
    }

    public void Despawn()
    {
        if (owner == null)
        {
            gameObject.SetActive(false);
            return;
        }

        lastDisableFrame = Time.frameCount;
        owner.Return(gameObject);
    }

    private void OnDisable ()
    {
        if (owner != null && lastDisableFrame != Time.frameCount)
        {
            lastDisableFrame = Time.frameCount;
            owner.Return(gameObject);
        }
    }
}
