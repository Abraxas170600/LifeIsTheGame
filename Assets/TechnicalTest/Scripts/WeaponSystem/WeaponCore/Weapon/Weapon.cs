using TechnicalTest.System.WeaponSystem.Data;
using UnityEngine;

namespace TechnicalTest.System.WeaponSystem.WeaponCore
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponType weaponType;
        [SerializeField] private GameObject triggerDetector;

        private Rigidbody weaponRigidbody;
        private BoxCollider weaponCollider;
        private SphereCollider weaponTrigger;

        private uint weaponDamage;
        private float weaponAttackSpeed;

        public void Set(uint weaponDamage, float weaponAttackSpeed)
        {
            this.weaponDamage = weaponDamage;
            this.weaponAttackSpeed = weaponAttackSpeed;
        }
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
        private bool GetWeaponComponents()
        {
            weaponRigidbody = GetComponent<Rigidbody>();
            weaponCollider = GetComponent<BoxCollider>();
            weaponTrigger = triggerDetector.GetComponent<SphereCollider>();

            return weaponRigidbody && weaponCollider && weaponTrigger;
        }
        public string WeaponName() => weaponType.ToString();
    }
}
