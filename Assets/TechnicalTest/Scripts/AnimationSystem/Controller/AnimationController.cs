using TechnicalTest.System.Utility.DataManagement;
using UnityEngine;

namespace TechnicalTest.System.AnimationSystem.Controller
{
    /// <summary>
    /// Class that manages the main actions of the animation system.
    /// </summary>
    public class AnimationController : MonoBehaviour
    {
        [Header("Animator dependences")]

        [Tooltip("Animator obtained from the character in the scene")]
        private Animator characterAnimator = null;
        [Tooltip("Name of the running animation")]
        private string currentAnimation = null;

        private void Start()
        {
            Initialize();
            LoadAnimation();
        }
        private void Initialize()
        {
            characterAnimator = GetComponent<Animator>();
        }
        public void EnableAnimation(string animationName)
        {
            if (characterAnimator == null)
            {
                Debug.LogError("Animator component not found!");
                return;
            }
            else
            {
                currentAnimation = animationName;
                characterAnimator.Play(currentAnimation);
            }
        }
        public void SaveAnimation()
        {
            if (string.IsNullOrEmpty(currentAnimation))
            {
                Debug.LogError("No animation has been selected");
                return;
            }

            SaveAndLoadUtility.SaveValue("SelectedAnimation", currentAnimation);
        }
        public void LoadAnimation()
        {
            string animationName = SaveAndLoadUtility.LoadValue("SelectedAnimation");

            if (string.IsNullOrEmpty(animationName))
                return;
            else
                EnableAnimation(animationName);
        }
    }
}
