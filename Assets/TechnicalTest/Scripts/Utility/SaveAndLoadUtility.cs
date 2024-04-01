using UnityEngine;

namespace TechnicalTest.System.Utility.DataManagement
{
    /// <summary>
    /// Utility class that saves and loads necessary information between scenes and game sessions.
    /// </summary>
    public static class SaveAndLoadUtility
    {
        public static void SaveValue<T>(string key, T value)
        {
            if (string.IsNullOrEmpty(key) || value == null)
            {
                Debug.LogError("Key or value not found, cannot be saved");
            }

            PlayerPrefs.SetString(key, value.ToString());
            Debug.Log($"Save {key} with value: {value}");
        }
        public static string LoadValue(string key)
        {
            string value = PlayerPrefs.GetString(key);

            if(string.IsNullOrEmpty(value))
            {
                Debug.LogError("Value not found, cannot be loaded");
                return null;
            }

            Debug.Log($"Load {key}");

            return value;
        }
    }
}
