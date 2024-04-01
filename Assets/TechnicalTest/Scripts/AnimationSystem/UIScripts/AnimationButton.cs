using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TechnicalTest.System.AnimationSystem.Controller;
using TechnicalTest.System.Utility.Listener;

namespace TechnicalTest.System.AnimationSystem.UI
{
    /// <summary>
    /// UI class representing the option buttons to choose the animation.
    /// </summary>
    public class AnimationButton : MonoBehaviour
    {
        private static AnimationButton lastSelectedButton;
        private static bool selectAnimationButtonActivated = false;

        public void InitializeButton(string animationName, AnimationController animationController)
        {
            if (string.IsNullOrEmpty(animationName))
            {
                Debug.LogError("Animation name is empty or null!");
                return;
            }

            SetupButton(animationName, animationController);
        }
        private void SetupButton(string animationName, AnimationController animationController)
        {
            Button animationButton = GetComponent<Button>();
            TMP_Text animationNameText = transform.GetChild(0).GetComponent<TMP_Text>();
            GameObject selectedText = transform.GetChild(1).gameObject;

            if (animationButton == null)
            {
                Debug.LogError("AnimationButton component is missing!");
                return;
            }

            animationNameText.text = animationName;
            animationButton.onClick.AddListener(() => SelectButton(animationName, selectedText, animationController));
        }
        private void SelectButton(string animationName, GameObject selectedText, AnimationController animationController)
        {
            DeactivateLastSelectedButton();

            selectedText.SetActive(true);
            lastSelectedButton = this;

            animationController.EnableAnimation(animationName);

            ActivateSelectAnimationButton();
        }
        private void DeactivateLastSelectedButton()
        {
            if (lastSelectedButton != null)
                lastSelectedButton.transform.GetChild(1).gameObject.SetActive(false);
        }
        private void ActivateSelectAnimationButton()
        {
            SelectAnimationButton selectAnimationButton = ListenerUtility.FindComponent<SelectAnimationButton>();

            if (ShouldActivateSelectAnimationButton(selectAnimationButton))
            {
                ActivateButton(selectAnimationButton.gameObject.GetComponent<Button>());
                selectAnimationButtonActivated = true;
            }
        }
        private bool ShouldActivateSelectAnimationButton(SelectAnimationButton selectAnimationButton)
        {
            return selectAnimationButton != null && !selectAnimationButtonActivated;
        }
        private void ActivateButton(Button button)
        {
            if (button != null)
                button.interactable = true;
        }
    }
}
