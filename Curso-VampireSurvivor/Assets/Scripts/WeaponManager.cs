using UnityEngine;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] weaponPrefabs;
    public Transform weaponParent;

    List <BaseWeapon> weapons = new();

    public void AddWeapon (int index)
    {
        if (index < 0 || index >= weaponPrefabs.Length) return;

        var prefab = weaponPrefabs[index];
        var existing = weapons.Find(w => w.weaponName == prefab.name);

        if (existing != null)
        {
            existing.Upgrade();
        }
        else
        {
            var go = Instantiate(prefab, weaponParent);
            BaseWeapon baseWeapon = go.GetComponent<BaseWeapon>();
            baseWeapon.owner = weaponParent.gameObject;
            weapons.Add(baseWeapon);
        }
    }
}
