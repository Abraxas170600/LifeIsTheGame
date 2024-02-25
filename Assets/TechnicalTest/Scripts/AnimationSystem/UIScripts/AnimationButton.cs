using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TechnicalTest.System.AnimationSystem.Controller;

namespace TechnicalTest.System.AnimationSystem.UI
{
    public class AnimationButton : MonoBehaviour
    {
        public void SetValues(string animationName, AnimationController animationController)
        {
            if (animationName == null || animationName == "")
            {
                Debug.LogError("Animation name is empty or null!");
                return;
            }

            InitializeButton(animationName, animationController);
        }
        private void InitializeButton(string animationName, AnimationController animationController)
        {
            Button animationButton = GetComponent<Button>();
            TMP_Text animationNameText = gameObject.transform.GetChild(0).GetComponent<TMP_Text>();

            if (animationButton == null || animationNameText == null)
            {
                Debug.LogError("Some AnimationButton component is missing!");
                return;
            }

            animationNameText.text = animationName;
            animationButton.onClick.AddListener(() => animationController.EnableAnimation(animationName));
        }
    }
}
