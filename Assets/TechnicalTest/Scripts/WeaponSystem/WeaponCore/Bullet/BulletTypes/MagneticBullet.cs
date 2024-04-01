using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TechnicalTest.System.WeaponSystem.WeaponCore
{
    public class MagneticBullet : Bullet
    {
        [SerializeField] private LayerMask magneticLayer;
        [SerializeField] private float magneticStrength = 5f;
        [SerializeField] private float magneticRange = 5f;
        public override void Initialize(uint weaponDamage)
        {
            base.Initialize(weaponDamage);
        }
        public override void SpawnBullet(GameObject bulletSpawn, Transform weaponTransform)
        {
            base.SpawnBullet(bulletSpawn, weaponTransform);
        }
        protected override void BulletAction()
        {
            if (enabled)
            {
                base.BulletAction();

                Collider[] hitColliders = Physics.OverlapSphere(transform.position, magneticRange, magneticLayer);
                foreach (Collider hitCollider in hitColliders)
                {
                    Rigidbody objectRigidbody = hitCollider.GetComponent<Rigidbody>();
                    if (!objectRigidbody.isKinematic)
                    {
                        Vector3 forceDirection = transform.position - hitCollider.transform.position;
                        objectRigidbody.AddForce(forceDirection.normalized * magneticStrength);
                    }
                }
            }
        }
    }
}
