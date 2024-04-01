using TechnicalTest.System.WeaponSystem.WeaponCore;
using UnityEngine;
using UnityEngine.Events;

namespace TechnicalTest.System.WeaponSystem.Controller
{
    /// <summary>
    /// Class that detects weapons through triggers and active external script events according to the detection status.
    /// </summary>
    public class WeaponEventsTrigger : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Weapon> triggerEnterEvent;
        [SerializeField] private UnityEvent triggerExitEvent;

        private Weapon currentWeapon;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Weapon"))
            {
                currentWeapon = other.gameObject.GetComponentInParent<Weapon>();
                triggerEnterEvent.Invoke(currentWeapon);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Weapon"))
            {
                triggerExitEvent.Invoke();
                currentWeapon = null;
            }
        }
    }
}
