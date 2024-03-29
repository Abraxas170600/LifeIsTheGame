using TechnicalTest.System.WeaponSystem.Controller;
using UnityEngine;
using UnityEngine.Events;

public class WeaponEventsTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent<WeaponDrop> triggerEnterEvent;
    [SerializeField] private UnityEvent triggerExitEvent;

    private WeaponDrop currentWeapon;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            currentWeapon = other.gameObject.GetComponentInParent<WeaponDrop>();
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
