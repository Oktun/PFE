using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DefaultWeapon : MonoBehaviour
{
    [SerializeField] protected bool isMelee = false;
    public bool IsMelee => isMelee;

    [Header("Weapon equip & Unequip Settings")]
    [SerializeField] protected Transform equipPos;
    [SerializeField] protected Transform unEquipPos;
    [Space]
    [SerializeField] protected Transform unEquipParent;
    [SerializeField] protected Transform equipParent;

    public virtual void EquipWeapon()
    {
        transform.position = equipPos.position;
        transform.rotation = equipPos.rotation;
        transform.parent = equipParent;
    }

    public virtual void UnEquipWeapon()
    {
        transform.position = unEquipPos.position;
        transform.rotation = unEquipPos.rotation;
        transform.parent = unEquipParent;
    }

    public abstract void Fire(Vector3 vector3);
}
