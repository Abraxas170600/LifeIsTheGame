using TechnicalTest.Core.Data;
using UnityEngine;

namespace TechnicalTest.Core
{
    public class GameCore : MonoBehaviour
    {
        [SerializeField] private GameCoreSO gameCoreSO;
        public GameCoreSO GameCoreSO { get => gameCoreSO; set => gameCoreSO = value; }

        #region Singleton
        public static GameCore Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }
        #endregion
    }
}