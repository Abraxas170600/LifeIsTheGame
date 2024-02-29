using UnityEngine;
using TechnicalTest.Core;

namespace TechnicalTest.System.Utility.Listener
{
    public static class ListenerUtility
    {
        public static T GetSystemSO<T>(string systemName) where T : ScriptableObject
        {
            GameCore gamecore = GameCore.Instance;

            for (int i = 0; i < gamecore.GameCoreSO.GameCoreData.Length; i++)
            {
                if (gamecore.GameCoreSO.GameCoreData[i].systemSOName == systemName)
                {
                    return (T)gamecore.GameCoreSO.GameCoreData[i].systemSO;
                }
            }

            Debug.LogError("SystemSO not found!");
            return null;
        }
        public static T FindComponent<T>() where T : MonoBehaviour
        {
            T component = Object.FindObjectOfType<T>();

            if (component == null)
            {
                Debug.LogError($"{typeof(T).Name} component not found!");
                return null;
            }

            return component;
        }
    }
}
