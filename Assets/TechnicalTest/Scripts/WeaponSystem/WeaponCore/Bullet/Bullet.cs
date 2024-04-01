using UnityEngine;
using DG.Tweening;
using TechnicalTest.Effects;

namespace TechnicalTest.System.WeaponSystem.WeaponCore
{
    /// <summary>
    /// Class inherited by all weapon bullets, controls the main actions of the bullets.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]
    public abstract class Bullet : MonoBehaviour
    {
        [SerializeField] protected float bulletSpeed;
        [SerializeField] protected float bulletTimeLife;

        protected Rigidbody bulletRigidbody;

        protected uint bulletDamage;

        protected Transform weaponTransform;
        protected TrailRenderer trail;

        protected VFXController currentBulletParticle;

        public virtual void Initialize(uint weaponDamage)
        {
            bulletRigidbody = GetComponent<Rigidbody>();
            transform.localScale = Vector3.zero;
            bulletDamage = weaponDamage;
        }
        public virtual void SpawnBullet(GameObject bulletSpawn, Transform weaponTransform, VFXController bulletParticle)
        {
            transform.position = bulletSpawn.transform.position;
            transform.rotation = weaponTransform.transform.rotation;

            currentBulletParticle = bulletParticle;
            currentBulletParticle.transform.position = transform.position;
            currentBulletParticle.transform.SetParent(transform);

            this.weaponTransform = weaponTransform;
            bulletTimeLife = 0f;

            Vector3 finalSize = new Vector3(0.001f, 0.001f, 0.001f);
            transform.DOScale(finalSize, 0.4f).SetEase(Ease.OutElastic);

            trail = GetComponent<TrailRenderer>();
            trail.Clear();

            bulletRigidbody.AddForce(weaponTransform.forward * bulletSpeed, ForceMode.VelocityChange);
        }
        public virtual uint BulletDamage => bulletDamage;
        protected virtual void FixedUpdate()
        {
            BulletAction();
        }
        protected virtual void BulletAction()
        {
            bulletTimeLife += Time.fixedDeltaTime;

            if (bulletTimeLife >= 4f)
                DesactiveBullet();
        }
        private void OnCollisionEnter()
        {
            DesactiveBullet();
        }
        protected virtual void DesactiveBullet()
        {
            currentBulletParticle.transform.parent = null;
            currentBulletParticle.ActiveParticle();
            transform.DOScale(Vector3.zero, 0.05f).SetEase(Ease.OutQuad).OnComplete(FinishActions);
        }
        private void FinishActions()
        {
            bulletRigidbody.velocity = Vector3.zero;
            transform.localScale = Vector3.zero;
            gameObject.SetActive(false);
        }
        protected void TrailColor(Color color)
        {
            trail.startColor = color;
            trail.endColor = color;
        }
    }
}
