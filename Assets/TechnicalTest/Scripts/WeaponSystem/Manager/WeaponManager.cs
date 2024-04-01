using TechnicalTest.Core.Manager;
using TechnicalTest.System.Utility.Listener;
using TechnicalTest.System.WeaponSystem.Controller;
using TechnicalTest.System.WeaponSystem.Data;
using TechnicalTest.System.WeaponSystem.UI;
using UnityEngine;

namespace TechnicalTest.System.WeaponSystem.Manager
{
    /// <summary>
    /// Manager that initializes the entire weapon system.
    /// </summary>
    [CreateAssetMenu(fileName = "WeaponManager", menuName = "Technical Test/Game Core/Manager/WeaponManager")]
    public class WeaponManager : ManagerSO
    {
        public override void InitializeSystem()
        {
            WeaponSO weaponSO = ListenerUtility.GetSystemSO<WeaponSO>("WeaponSO");
            WeaponController weaponController = ListenerUtility.FindComponent<WeaponController>();

            if (weaponController == null)
            {
                Debug.Log("Weapon Manager Desactived");
                return;
            }
            else
            {
                WeaponUI weaponUI = ListenerUtility.FindComponent<WeaponUI>();

                weaponController.Initialize(weaponSO);
            }
        }
    }
}
