using UnityEngine;

namespace TechnicalTest.System.AnimationSystem.Data
{
    [CreateAssetMenu(fileName = "AnimationData", menuName = "Technical Test/Animation System/Animation Data")]
    public class AnimationSO : ScriptableObject
    {
        [SerializeField] private AnimationData[] animationData;
        public AnimationData[] AnimationData { get => animationData; set => animationData = value; }
    }
}
