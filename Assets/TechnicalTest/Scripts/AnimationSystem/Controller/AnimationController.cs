using TechnicalTest.System.Utility.DataManagement;
using UnityEngine;

namespace TechnicalTest.System.AnimationSystem.Controller
{
    public class AnimationController : MonoBehaviour
    {
        private Animator characterAnimator = null;
        private string currentAnimation = null;

        private void Start()
        {
            Initialize();
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
            if (currentAnimation == null || currentAnimation == "")
            {
                Debug.LogError("No animation has been selected");
                return;
            }

            SaveAndLoadUtility.SaveValue("SelectedAnimation", currentAnimation);
        }
        public void LoadAnimation()
        {
            string animationName = SaveAndLoadUtility.LoadValue("SelectedAnimation");
            EnableAnimation(animationName);
        }
    }
}
