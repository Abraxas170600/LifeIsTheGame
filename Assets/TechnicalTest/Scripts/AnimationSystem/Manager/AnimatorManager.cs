using TechnicalTest.System.AnimationSystem.Controller;
using TechnicalTest.System.AnimationSystem.Data;
using TechnicalTest.System.AnimationSystem.UI;
using TechnicalTest.System.Utility.Listener;
using TechnicalTest.Core.Manager;
using UnityEngine;

namespace TechnicalTest.System.AnimationSystem.Manager
{
    /// <summary>
    /// Manager that initializes the entire animation system.
    /// </summary>
    [CreateAssetMenu(fileName = "AnimationManager", menuName = "Technical Test/Game Core/Manager/AnimationManager")]
    public class AnimatorManager : ManagerSO
    {
        public override void InitializeSystem()
        {
            AnimationSO animationSO = ListenerUtility.GetSystemSO<AnimationSO>("AnimationSO");
            AnimationController animationController = ListenerUtility.FindComponent<AnimationController>();

            if (animationController == null)
            {
                Debug.Log("Animator Manager Desactived");
                return;
            }
            else
            {
                AnimationUI animationUI = ListenerUtility.FindComponent<AnimationUI>();

                if(animationUI == null)
                    return;
                else
                    animationUI.Initialize(animationSO, animationController);
            }
        }
    }
}
