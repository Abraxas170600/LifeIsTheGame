using System;
using UnityEngine;

namespace TechnicalTest.System.WeaponSystem.Data
{
    [Serializable]
    public class WeaponData
    {
        public WeaponType weaponName;
        public uint weaponDamage;
        public float weaponAttackSpeed;
        public GameObject weaponBullet;
    }
}
