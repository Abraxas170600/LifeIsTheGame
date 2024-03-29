using TechnicalTest.System.WeaponSystem.Data;
using UnityEngine;

namespace TechnicalTest.System.WeaponSystem.Controller
{
    public class WeaponDrop : MonoBehaviour
    {
        [SerializeField] private WeaponType weaponType;

        private Weapon weaponObject;
        private uint weaponDamage;
        private float weaponAttackSpeed;

        public Weapon WeaponObject { get => weaponObject; set => weaponObject = value; }
        public uint WeaponDamage { get => weaponDamage; set => weaponDamage = value; }
        public float WeaponAttackSpeed { get => weaponAttackSpeed; set => weaponAttackSpeed = value; }

        public void Set(Weapon weaponObject, uint weaponDamage, float weaponAttackSpeed)
        {
            this.WeaponObject = weaponObject;
            this.WeaponDamage = weaponDamage;
            this.WeaponAttackSpeed = weaponAttackSpeed;
        }
        public string WeaponName() => weaponType.ToString();
    }
}
