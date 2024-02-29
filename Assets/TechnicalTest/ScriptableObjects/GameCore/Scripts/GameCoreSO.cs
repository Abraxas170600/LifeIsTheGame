using UnityEngine;

namespace TechnicalTest.Core.Data
{
    [CreateAssetMenu(fileName = "GameCoreData", menuName = "Technical Test/Game Core/Game Core Data")]
    public class GameCoreSO : ScriptableObject
    {
        [SerializeField] private GameCoreData[] gameCoreData;
        public GameCoreData[] GameCoreData { get => gameCoreData; set => gameCoreData = value; }
    }
}

