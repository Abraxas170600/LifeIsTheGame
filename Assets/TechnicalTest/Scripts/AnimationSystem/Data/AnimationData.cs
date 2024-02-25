using UnityEngine;
using System;

namespace TechnicalTest.System.AnimationSystem.Data
{
    [Serializable]
    public class AnimationData
    {
        [SerializeField] private string animationName;
        public string AnimationName { get => animationName; set => animationName = value; }
    }
}
