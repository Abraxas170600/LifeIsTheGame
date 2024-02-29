using UnityEngine;

namespace TechnicalTest.System.AnimationSystem.Data
{
    [CreateAssetMenu(fileName = "AnimationData", menuName = "Technical Test/Animation System/Animation Data")]
    public class AnimationSO : ScriptableObject
    {
        public AnimationData[] animationData;
    }
}
