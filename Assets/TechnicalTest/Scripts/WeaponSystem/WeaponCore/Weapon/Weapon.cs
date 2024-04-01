using System.Collections.Generic;
using TechnicalTest.System.WeaponSystem.Data;
using UnityEngine;

namespace TechnicalTest.System.WeaponSystem.WeaponCore
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponType weaponType;

        private Rigidbody weaponRigidbody;
        private BoxCollider weaponCollider;
        private SphereCollider weaponTrigger;

        private float weaponAttackSpeed;
        private float attackSpeedTimer;
        private bool canShoot;

        [SerializeField] private GameObject bulletSpawn;
        [SerializeField] private int bulletSize = 10;
        private readonly List<GameObject> bullets = new List<GameObject>();


        #region Initialize
        public void InitializeWeapon(uint weaponDamage, float weaponAttackSpeed, Bullet weaponBullet)
        {
            this.weaponAttackSpeed = weaponAttackSpeed;
            InstantiateWeaponBullets(weaponBullet, weaponDamage, bulletSize);
        }
        private void InstantiateWeaponBullets(Bullet weaponBullet, uint weaponDamage, int bulletAmount)
        {
            for (int i = 0; i < bulletAmount; i++)
            {
                GameObject bullet = Instantiate(weaponBullet.gameObject);

                bullet.GetComponent<Bullet>().Initialize(weaponDamage);
                bullet.SetActive(false);
                bullet.transform.SetParent(bulletSpawn.transform);

                bullets.Add(bullet);
            }
        }
        #endregion

        #region PickUp and Drop
        public void PickUp()
        {
            if (!GetWeaponComponents())
                Debug.LogError("Weapon components not found");
            else
            {
                transform.localPosition = Vector3.zero;
                transform.localEulerAngles = Vector3.zero;

                weaponCollider.enabled = false;
                weaponTrigger.enabled = false;

                weaponRigidbody.isKinematic = true;
                weaponRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            }
        }
        public void Drop(Transform weaponParent)
        {
            if (weaponRigidbody != null)
            {
                transform.localEulerAngles = new Vector3(90f, 0f, 0f);

                weaponCollider.enabled = true;
                weaponTrigger.enabled = true;

                weaponRigidbody.isKinematic = false;
                weaponRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
                weaponRigidbody.AddForce(weaponParent.forward * 7f);
            }
        }
        #endregion

        #region Get Weapon Info
        private bool GetWeaponComponents()
        {
            weaponRigidbody = GetComponent<Rigidbody>();
            weaponCollider = GetComponent<BoxCollider>();
            weaponTrigger = transform.GetComponentInChildren<SphereCollider>();

            return weaponRigidbody && weaponCollider && weaponTrigger;
        }
        public string GetWeaponName() => weaponType.ToString();
        #endregion

        #region Weapon Bullets
        private void Update()
        {
            attackSpeedTimer += Time.deltaTime;

            if (attackSpeedTimer >= weaponAttackSpeed)
                canShoot = true;
        }
        public void RequestBullet()
        {
            if (canShoot)
            {
                for (int i = 0; i < bullets.Count; i++)
                {
                    if (!bullets[i].activeSelf)
                    {
                        bullets[i].SetActive(true);
                        bullets[i].transform.parent = null;
                        bullets[i].GetComponent<Bullet>().SpawnBullet(bulletSpawn, this.transform);

                        attackSpeedTimer = 0;
                        canShoot = false;
                        return;
                    }
                }
            }
            else
            {
                Debug.Log("Attack speed limit");
            }

        }
        #endregion
    }
}
