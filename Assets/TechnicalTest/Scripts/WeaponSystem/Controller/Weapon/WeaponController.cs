using System.Collections.Generic;
using TechnicalTest.System.InputSystem;
using TechnicalTest.System.WeaponSystem.Data;
using TechnicalTest.System.WeaponSystem.WeaponCore;
using UnityEngine;
using UnityEngine.Events;

namespace TechnicalTest.System.WeaponSystem.Controller
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private GameObject weaponContainer;
        [SerializeField] private GameObject weaponDropContainer;
        [SerializeField] private UnityEvent interactEvents;

        private readonly Dictionary<string, Weapon> weapons = new Dictionary<string, Weapon>();

        private Weapon currentDroppedWeapon;
        private GameObject currentWeapon;

        private uint weaponDamage;
        private float weaponAttackSpeed;

        private InputController input;

        #region Initialize
        public void Initialize(WeaponSO weaponSO)
        {
            input = GetComponent<InputController>();

            ConfiguratedWeapons(weaponSO);
        }
        private void ConfiguratedWeapons(WeaponSO weaponSO)
        {
            foreach (Weapon weapon in FindObjectsOfType<Weapon>())
            {
                weapons.Add(weapon.GetWeaponName(), weapon);
            }

            foreach (var weaponData in weaponSO.weaponData)
            {
                if (weapons.ContainsKey(weaponData.weaponName.ToString()))
                {
                    Weapon weapon = weapons[weaponData.weaponName.ToString()];
                    weapon.InitializeWeapon(weaponData.weaponDamage, weaponData.weaponAttackSpeed, weaponData.weaponBullet);
                }
            }
        }
        #endregion

        private void Update()
        {
            Shoot();
            Interact();
        }

        #region Input Methods
        private void Shoot()
        {
            if (input.shoot)
            {
                if (currentWeapon != null)
                    currentWeapon.GetComponent<Weapon>().RequestBullet();

                input.shoot = false;
            }
        }
        private void Interact()
        {
            if (input.interact)
            {
                GetWeapon();
                input.interact = false;
            }
        }
        #endregion

        private void GetWeapon()
        {
            if (currentDroppedWeapon == null)
                return;

            foreach (var weaponValue in weapons)
            {
                Weapon weapon = weaponValue.Value;
                if (weapon.GetWeaponName() == currentDroppedWeapon.GetWeaponName())
                {
                    if (currentWeapon != null)
                    {
                        currentWeapon.transform.SetParent(weaponDropContainer.transform);
                        currentWeapon.GetComponent<Weapon>().Drop(transform);
                        currentWeapon = null;
                    }

                    interactEvents.Invoke();
                    currentWeapon = currentDroppedWeapon.gameObject;
                    currentWeapon.transform.SetParent(weaponContainer.transform);

                    currentWeapon.GetComponent<Weapon>().PickUp();

                    break;
                }
            }
        }

        #region Trigger Methods
        public void GetWeaponComponent(Weapon weaponComponent)
        {
            if (weaponComponent == null) return;
            else
            {
                currentDroppedWeapon = weaponComponent;
            }
        }
        public void DeleteWeaponDrop()
        {
            currentDroppedWeapon = null;
        }
        #endregion
    }
}
