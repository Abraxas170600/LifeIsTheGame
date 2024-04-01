using UnityEngine;
using UnityEngine.UI;
using TechnicalTest.System.AnimationSystem.Controller;

namespace TechnicalTest.System.AnimationSystem.UI
{
    /// <summary>
    /// UI class that controls the button that chooses the animation and switches between scenes.
    /// </summary>
    public class SelectAnimationButton : MonoBehaviour
    {
        public void InitializeButton(AnimationController animationController)
        {
            Button selectAnimationButton = GetComponent<Button>();

            if (selectAnimationButton == null)
            {
                Debug.LogError("SelectAnimationButton component is missing!");
                return;
            }

            selectAnimationButton.onClick.AddListener(() => animationController.SaveAnimation());
            selectAnimationButton.interactable = false;
        }
    }
}
