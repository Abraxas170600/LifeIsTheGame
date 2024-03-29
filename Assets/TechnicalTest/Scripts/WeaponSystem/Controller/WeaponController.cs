using System.Collections.Generic;
using TechnicalTest.System.InputSystem;
using TechnicalTest.System.WeaponSystem.Data;
using UnityEngine;
using UnityEngine.Events;

namespace TechnicalTest.System.WeaponSystem.Controller
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private GameObject weaponContainer;
        [SerializeField] private GameObject weaponDropContainer;
        [SerializeField] private UnityEvent interactEvents;

        private InputController input;

        private readonly List<Weapon> obtainedWeapons = new List<Weapon>();
        private readonly Dictionary<string, WeaponDrop> weaponDrops = new Dictionary<string, WeaponDrop>();

        private WeaponDrop currentWeaponDrop;
        //private WeaponDrop oldWeaponDrop;

        private Weapon currentWeapon;

        private uint weaponDamage;
        private float weaponAttackSpeed;

        public void Initialize(WeaponSO weaponSO)
        {
            input = GetComponent<InputController>();

            ConfiguratedWeapons(weaponSO);
        }
        private void ConfiguratedWeapons(WeaponSO weaponSO)
        {
            foreach (WeaponDrop weaponDrop in FindObjectsOfType<WeaponDrop>())
            {
                weaponDrops.Add(weaponDrop.WeaponName(), weaponDrop);
            }

            foreach (var weaponData in weaponSO.weaponData)
            {
                if (weaponDrops.ContainsKey(weaponData.weaponName.ToString()))
                {
                    WeaponDrop weaponDrop = weaponDrops[weaponData.weaponName.ToString()];
                    weaponDrop.Set(weaponData.weaponObject, weaponData.weaponDamage, weaponData.weaponAttackSpeed);
                }
            }
        }
        private void Update()
        {
            Shoot();
            Interact();
        }
        private void Shoot()
        {
            if (input.shoot)
            {
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
        private void GetWeapon()
        {
            if (currentWeaponDrop == null)
                return;

            if (obtainedWeapons.Count > 0)
            {
                foreach (var weapon in obtainedWeapons)
                    weapon.gameObject.SetActive(false);
            }

            // TODO Arreglar el sistema de cambio de arma
            //ThrowOldWeaponDrop();

            InstantiateWeapon();
        }
        private void InstantiateWeapon()
        {
            foreach (var weaponValue in weaponDrops)
            {
                WeaponDrop weaponDrop = weaponValue.Value;

                if (weaponDrop.WeaponName() == currentWeaponDrop.WeaponName())
                {
                    obtainedWeapons.Add(currentWeapon = Instantiate(weaponDrop.WeaponObject, weaponContainer.transform));
                    //currentWeapon.Set(weaponDrop.WeaponDamage, weaponDrop.WeaponAttackSpeed);
                    currentWeapon.gameObject.SetActive(true);
                    interactEvents.Invoke();

                    // TODO Arreglar el sistema de cambio de arma
                    Destroy(currentWeaponDrop.gameObject);

                    break;
                }
            }
        }

        // TODO Arreglar el sistema de cambio de arma
        //private void ThrowOldWeaponDrop()
        //{
        //    if (oldWeaponDrop != null)
        //    {
        //        WeaponDrop weaponInstance = Instantiate(oldWeaponDrop, weaponContainer.transform.position, transform.rotation);
        //        weaponInstance.transform.parent = weaponDropContainer.transform;

        //        Rigidbody weaponInstanceRigidbody = weaponInstance.GetComponent<Rigidbody>();
        //        if (weaponInstanceRigidbody != null)
        //        {
        //            weaponInstanceRigidbody.AddForce(transform.forward * 7f);
        //        }
        //    }

        //    oldWeaponDrop = currentWeaponDrop;
        //}

        #region Trigger Methods
        public void GetWeaponDrop(WeaponDrop weaponDrop)
        {
            if (weaponDrop == null) return;
            else
            {
                currentWeaponDrop = weaponDrop;
            }
        }
        public void DeleteWeaponDrop()
        {
            currentWeaponDrop = null;
        }
        #endregion
    }
}
