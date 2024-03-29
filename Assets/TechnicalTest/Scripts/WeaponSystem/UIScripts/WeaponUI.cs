using TechnicalTest.System.WeaponSystem.Controller;
using UnityEngine;

namespace TechnicalTest.System.WeaponSystem.UI
{
    public class WeaponUI : MonoBehaviour
    {
        [SerializeField] private WeaponText weaponText;

        public void ActiveWeaponText(WeaponDrop weaponDrop)
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
