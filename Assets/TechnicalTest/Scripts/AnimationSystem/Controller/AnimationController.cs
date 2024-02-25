using UnityEngine;

namespace TechnicalTest.System.AnimationSystem.Controller
{
    public class AnimationController : MonoBehaviour
    {
        private Animator _characterAnimator;
        private void Start()
        {
            Initialize();
        }
        private void Initialize()
        {
            _characterAnimator = GetComponent<Animator>();
        }
        public void EnableAnimation(string animationName)
        {
            if (_characterAnimator == null)
            {
                Debug.LogError("Animator component not found!");
                return;
            }
            else
                _characterAnimator.Play(animationName);
        }
    }
}
