using System;
using UnityEngine;

namespace TechnicalTest.Core.Manager
{
    /// <summary>
    /// Class inherited by all game managers
    /// </summary>
    [Serializable]
    public abstract class ManagerSO : ScriptableObject
    {
        public abstract void InitializeSystem();
    }
}
