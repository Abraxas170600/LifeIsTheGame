using TechnicalTest.Effects;
using UnityEngine;

namespace TechnicalTest.System.WeaponSystem.WeaponCore
{
    /// <summary>
    /// bullet with parabolic movement that increases the range according to the angle of fire. High damage and has medium range.
    /// </summary>
    public class ParabolicBullet : Bullet
    {
        private const float gravity = 9.81f;
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

                Vector3 gravityAcceleration = new Vector3(0, -gravity, 0);
                Vector3 newVelocity = bulletRigidbody.velocity + gravityAcceleration * Time.fixedDeltaTime;
                bulletRigidbody.velocity = newVelocity;
            }
        }
    }
}
