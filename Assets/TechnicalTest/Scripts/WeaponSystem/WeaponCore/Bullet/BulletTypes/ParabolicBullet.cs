using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TechnicalTest.System.WeaponSystem.WeaponCore
{
    public class ParabolicBullet : Bullet
    {
        private const float gravity = 9.81f;
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

                Vector3 gravityAcceleration = new Vector3(0, -gravity, 0);
                Vector3 newVelocity = bulletRigidbody.velocity + gravityAcceleration * Time.fixedDeltaTime;
                bulletRigidbody.velocity = newVelocity;
            }
        }
    }
}
