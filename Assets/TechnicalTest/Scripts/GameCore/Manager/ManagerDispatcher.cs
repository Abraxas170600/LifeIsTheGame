using System;
using UnityEngine;

namespace TechnicalTest.Core.Manager
{
    [Serializable]
    public class ManagerType
    {
        public string managerName;
        public ManagerSO managerSO;
        public bool activeSystem;
    }
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
                if (managerType[i].activeSystem)
                    managerType[i].managerSO.InitializeSystem();
            }
        }
    }
}
