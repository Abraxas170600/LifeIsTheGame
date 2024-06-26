using TechnicalTest.System.WeaponSystem.WeaponCore;
using System;

namespace TechnicalTest.System.WeaponSystem.Data
{
    [Serializable]
    public class WeaponData
    {
        public WeaponType weaponName;
        public uint weaponDamage;
        public float weaponAttackSpeed;
        public Bullet weaponBullet;
    }
}
