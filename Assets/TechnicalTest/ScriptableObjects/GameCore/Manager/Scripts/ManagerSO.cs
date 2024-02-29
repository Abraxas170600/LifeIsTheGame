using System;
using UnityEngine;

namespace TechnicalTest.Core.Manager
{
    [Serializable]
    public abstract class ManagerSO : ScriptableObject
    {
        public abstract void InitializeSystem();
    }
}
