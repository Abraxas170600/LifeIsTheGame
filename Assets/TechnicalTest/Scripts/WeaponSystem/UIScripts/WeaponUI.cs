using TechnicalTest.System.WeaponSystem.WeaponCore;
using UnityEngine;

namespace TechnicalTest.System.WeaponSystem.UI
{
    /// <summary>
    /// UI class that controls and initialize all user interface actions of the weapon system.
    /// </summary>
    public class WeaponUI : MonoBehaviour
    {
        [SerializeField] private WeaponText weaponText;

        public void ActiveWeaponText(Weapon weaponDrop)
        {
            string weaponName = weaponDrop.GetWeaponName();
            weaponText.Active(weaponName);
        }
        public void DesactiveWeaponText()
        {
            weaponText.Desactive();
        }
    }
}
