using UnityEngine;
using UnityEngine.SceneManagement;

namespace TechnicalTest.Core.UI
{
    /// <summary>
    /// UI class used to switch between scenes.
    /// </summary>
    public class LoadScene : MonoBehaviour
    {
        public void LoadSceneWithName(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }
    }
}
