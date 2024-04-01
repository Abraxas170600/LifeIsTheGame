using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TechnicalTest.System.WeaponSystem.WeaponCore
{
    /// <summary>
    /// bullets aimed at the nearest target. Low damage but high range and speed.
    /// </summary>
    public class CustomBullet : Bullet
    {
        [SerializeField] private LayerMask targetLayer;
        [SerializeField] private float detectionRadius = 10f;

        private Transform target;
        public override void Initialize(uint weaponDamage)
        {
            base.Initialize(weaponDamage);
        }
        public override void SpawnBullet(GameObject bulletSpawn, Transform weaponTransform)
        {
            base.SpawnBullet(bulletSpawn, weaponTransform);

            TrailColor(Color.white);

            bulletSpeed = 10;
            target = null;
        }
        protected override void BulletAction()
        {
            if (enabled)
            {
                base.BulletAction();

                Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, targetLayer);

                float closestDistance = Mathf.Infinity;
                foreach (Collider hitCollider in hitColliders)
                {
                    float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        target = hitCollider.transform;
                    }
                }

                if (target != null)
                {
                    Vector3 direction = (target.position - transform.position).normalized;
                    TrailColor(Color.red);

                    bulletSpeed += Time.fixedDeltaTime * 3f;
                    bulletRigidbody.velocity = direction * bulletSpeed;
                }
            }
        }
    }
}
