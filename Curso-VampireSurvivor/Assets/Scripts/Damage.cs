using UnityEngine;

[System.Serializable]
public struct Damage
{
    public float amount;
    public GameObject source;
    public Vector3 position;
    public Vector3 direction;
    public string tagExtra;

    public Damage (float amount, GameObject source = null, Vector3 position = default, Vector3 direction = default, string tagExtra = "")
    {
        this.amount = amount;
        this.source = source;
        this.position = position;
        this.direction = direction;
        this.tagExtra = tagExtra;
    }
}
