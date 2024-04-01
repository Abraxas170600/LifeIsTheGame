using System.Collections.Generic;
using UnityEngine;
using TechnicalTest.System.AnimationSystem.Data;
using TechnicalTest.System.AnimationSystem.Controller;

namespace TechnicalTest.System.AnimationSystem.UI
{
    /// <summary>
    /// UI class that controls and initialize all user interface actions of the animation system.
    /// </summary>
    public class AnimationUI : MonoBehaviour
    {
        [SerializeField] private AnimationButton animationButton;
        [SerializeField] private SelectAnimationButton selectAnimationButton;

        [SerializeField] private GameObject animationButtonContainer;

        private readonly List<AnimationButton> animationButtons = new List<AnimationButton>();

        public void Initialize(AnimationSO animationSO, AnimationController animationController)
        {
            InitializeButtons(animationSO, animationController);
        }
        private void InitializeButtons(AnimationSO animationSO, AnimationController animationController)
        {
            InstantiateAnimationButtons(animationSO, animationController);
            selectAnimationButton.InitializeButton(animationController);
        }
        private void InstantiateAnimationButtons(AnimationSO animationSO, AnimationController animationController)
        {
            for (int i = 0; i < animationSO.animationData.Length; i++)
            {
                animationButtons.Add(Instantiate(animationButton, animationButtonContainer.transform));
                animationButtons[i].InitializeButton(animationSO.animationData[i].animationName, animationController);
            }
        }
    }
}
