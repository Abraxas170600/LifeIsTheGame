using System.Collections.Generic;
using UnityEngine;
using TechnicalTest.System.AnimationSystem.Data;
using TechnicalTest.System.AnimationSystem.Listeners;
using TechnicalTest.System.AnimationSystem.Controller;

namespace TechnicalTest.System.AnimationSystem.UI
{
    public class AnimationUI : MonoBehaviour
    {
        [SerializeField] private AnimationButton animationButton;
        [SerializeField] private GameObject animationButtonContainer;

        private List<AnimationButton> animationButtons = new List<AnimationButton>();

        private AnimationController animationController;
        private void Initialize(AnimationSO animationSO)
        {
            GetAnimationController();
            InstantiateAnimationButtons(animationSO);
        }
        private void GetAnimationController()
        {
            AnimationControllerListener animationControllerListener = GetComponent<AnimationControllerListener>();
            animationController = animationControllerListener.GetAnimationController();
        }
        private void InstantiateAnimationButtons(AnimationSO animationSO)
        {
            for (int i = 0; i < animationSO.AnimationData.Length; i++)
            {
                animationButtons.Add(Instantiate(animationButton, animationButtonContainer.transform));
                animationButtons[i].SetValues(animationSO.AnimationData[i].AnimationName, animationController);
            }
        }
    }
}
