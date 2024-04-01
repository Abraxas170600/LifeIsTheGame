using UnityEngine;
using TMPro;

namespace TechnicalTest.System.WeaponSystem.UI
{
    /// <summary>
    /// UI class that controls the actions of the text that is output when the system detects a weapon within the player's reach.
    /// </summary>
    public class WeaponText : MonoBehaviour
    {
        [SerializeField] private TMP_Text weaponText;

        public void Active(string weaponName)
        {
            ChangeText(weaponName);
            gameObject.SetActive(true);
        }

        public void Desactive()
        {
            gameObject.SetActive(false);
        }

        private void ChangeText(string weaponName)
        {
            weaponText.text = $"Press E to pick up {weaponName}";
        }
    }
}
