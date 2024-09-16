using UnityEngine;
using TechnicalTest.Effects;

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
        public override void SpawnBullet(GameObject bulletSpawn, Transform weaponTransform, VFXController bulletParticle)
        {
            base.SpawnBullet(bulletSpawn, weaponTransform, bulletParticle);

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
                    float distance = Vector3.Distance(transform.position, hitCollider.bounds.center);
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

                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 30f * Time.deltaTime);
                }
            }
        }
    }
}
