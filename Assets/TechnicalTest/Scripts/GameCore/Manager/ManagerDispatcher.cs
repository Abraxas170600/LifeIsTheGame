using System;
using UnityEngine;

namespace TechnicalTest.Core.Manager
{
    /// <summary>
    /// Class representing each manager in the game.
    /// </summary>
    [Serializable]
    public class ManagerType
    {
        public string managerName;
        public ManagerSO managerSO;
    }

    /// <summary>
    /// Class that initializes all game managers.
    /// </summary>
    public class ManagerDispatcher : MonoBehaviour
    {
        [SerializeField] private ManagerType[] managerType;
        private void Start()
        {
            InitializeManagers();
        }
        private void InitializeManagers()
        {
            for (int i = 0; i < managerType.Length; i++)
            {
                managerType[i].managerSO.InitializeSystem();
            }
        }
    }
}
