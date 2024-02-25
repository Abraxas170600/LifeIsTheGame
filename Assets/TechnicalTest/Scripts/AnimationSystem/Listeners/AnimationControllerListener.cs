using UnityEngine;
using TechnicalTest.System.AnimationSystem.Controller;

namespace TechnicalTest.System.AnimationSystem.Listeners
{
    public class AnimationControllerListener : MonoBehaviour
    {
        public AnimationController GetAnimationController()
        {
            AnimationController animationController = FindObjectOfType<AnimationController>();
            return animationController;
        }
    }
}
