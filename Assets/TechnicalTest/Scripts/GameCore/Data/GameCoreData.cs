using UnityEngine;
using System;

namespace TechnicalTest.GameCore.Data
{
    [Serializable]
    public class GameCoreData
    {
        [SerializeField] private string systemSOName;
        [SerializeField] private ScriptableObject systemSO;

        public string SystemSOName { get => systemSOName; set => systemSOName = value; }
        public ScriptableObject SystemSO { get => systemSO; set => systemSO = value; }
    }
}
