using System;
using UnityEngine;

namespace TechnicalTest.System.WeaponSystem.Data
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Technical Test/Weapon System/WeaponData")]
    public class WeaponSO : ScriptableObject
    {
        public WeaponData[] weaponData;
    }
}
