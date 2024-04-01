using TechnicalTest.Effects;
using UnityEngine;

namespace TechnicalTest.System.WeaponSystem.WeaponCore
{
    /// <summary>
    /// magnetic weapons that attract dynamic objects in their environment. Have medium damage and short range.
    /// </summary>
    public class MagneticBullet : Bullet
    {
        [SerializeField] private LayerMask magneticLayer;
        [SerializeField] private float magneticStrength = 5f;
        [SerializeField] private float magneticRange = 5f;
        public override void Initialize(uint weaponDamage)
        {
            base.Initialize(weaponDamage);
        }
        public override void SpawnBullet(GameObject bulletSpawn, Transform weaponTransform, VFXController bulletParticle)
        {
            base.SpawnBullet(bulletSpawn, weaponTransform, bulletParticle);
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
