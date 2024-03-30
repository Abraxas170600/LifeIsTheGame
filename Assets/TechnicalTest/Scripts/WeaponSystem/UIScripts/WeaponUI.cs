using TechnicalTest.System.WeaponSystem.WeaponCore;
using UnityEngine;

namespace TechnicalTest.System.WeaponSystem.UI
{
    public class WeaponUI : MonoBehaviour
    {
        [SerializeField] private WeaponText weaponText;

        public void ActiveWeaponText(Weapon weaponDrop)
        {
            string weaponName = weaponDrop.WeaponName();
            weaponText.Active(weaponName);
        }
        public void DesactiveWeaponText()
        {
            weaponText.Desactive();
        }
    }
}
