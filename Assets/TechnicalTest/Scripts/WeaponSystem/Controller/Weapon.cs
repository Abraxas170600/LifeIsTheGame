using TechnicalTest.System.WeaponSystem.Data;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponType weaponType;
    //private uint weaponDamage;
    //private float weaponAttackSpeed;

    //public void Set(uint weaponDamage, float weaponAttackSpeed)
    //{
    //    this.weaponDamage = weaponDamage;
    //    this.weaponAttackSpeed = weaponAttackSpeed;
    //}
    public string WeaponName()
    {
        return weaponType.ToString();
    }
}
