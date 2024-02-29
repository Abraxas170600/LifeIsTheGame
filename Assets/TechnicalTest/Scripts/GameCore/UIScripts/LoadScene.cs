using UnityEngine;
using UnityEngine.SceneManagement;

namespace TechnicalTest.Core.UI
{
    public class LoadScene : MonoBehaviour
    {
        public void LoadSceneWithName(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }
    }
}
