using UnityEngine;
using DG.Tweening;

namespace TechnicalTest.System.WeaponSystem.WeaponCore
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]
    public abstract class Bullet : MonoBehaviour
    {
        [SerializeField] protected float bulletSpeed;
        [SerializeField] protected float bulletTimeLife;
        //[SerializeField] protected ParticleSystem VFXBullet;

        protected Rigidbody bulletRigidbody;

        protected uint bulletDamage;

        protected Transform weaponTransform;
        protected TrailRenderer trail;

        public virtual void Initialize(uint weaponDamage)
        {
            bulletRigidbody = GetComponent<Rigidbody>();
            transform.localScale = Vector3.zero;
            bulletDamage = weaponDamage;
        }
        public virtual void SpawnBullet(GameObject bulletSpawn, Transform weaponTransform)
        {
            transform.position = bulletSpawn.transform.position;
            transform.rotation = weaponTransform.transform.rotation;

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
