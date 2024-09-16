using System.Collections.Generic;
using TechnicalTest.Effects;
using TechnicalTest.System.WeaponSystem.Data;
using UnityEngine;

namespace TechnicalTest.System.WeaponSystem.WeaponCore
{
    /// <summary>
    /// Class that configures and controls the actions of each weapon according to the data of each one.
    /// </summary>
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
        [SerializeField] private int bulletAmount = 10;
        private readonly List<GameObject> bullets = new List<GameObject>();

        [SerializeField] private VFXController VFXBullet;
        private readonly List<GameObject> bulletParticles = new List<GameObject>();


        #region Initialize
        public void InitializeWeapon(uint weaponDamage, float weaponAttackSpeed, Bullet weaponBullet)
        {
            this.weaponAttackSpeed = weaponAttackSpeed;

            BulletsPool(weaponBullet, weaponDamage, bulletAmount);
            bulletParticlesPool(bulletAmount);
        }
        private void BulletsPool(Bullet weaponBullet, uint weaponDamage, int bulletAmount)
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
        private void bulletParticlesPool(int particleAmount)
        {
            for (int i = 0; i < particleAmount; i++)
            {
                GameObject bulletParticle = Instantiate(VFXBullet.gameObject);

                bulletParticle.SetActive(false);

                bulletParticles.Add(bulletParticle);
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
        public void RequestBullet(Animator playerAnimator)
        {
            if (canShoot)
            {
                for (int i = 0; i < bullets.Count; i++)
                {
                    if (!bullets[i].activeSelf)
                    {
                        bullets[i].SetActive(true);
                        bullets[i].transform.parent = null;
                        bullets[i].GetComponent<Bullet>().SpawnBullet(bulletSpawn, this.transform, RequestBulletParticle());

                        playerAnimator.Play("Shoot");

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
        private VFXController RequestBulletParticle()
        {
            for (int i = 0; i < bulletParticles.Count; i++)
            {
                if (!bulletParticles[i].activeSelf)
                {
                    bulletParticles[i].SetActive(true);
                    bulletParticles[i].transform.parent = null;
                    return bulletParticles[i].GetComponent<VFXController>();
                }
            }

            bulletParticlesPool(1);
            bulletParticles[bulletParticles.Count - 1].SetActive(true);
            return bulletParticles[bulletParticles.Count - 1].GetComponent<VFXController>();
        }
        #endregion
    }
}
