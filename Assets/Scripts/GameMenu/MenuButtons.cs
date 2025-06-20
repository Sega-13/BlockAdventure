using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameBlockAdv.GameMenu
{
    public class MenuButtons : MonoBehaviour
    {
        private void Awake()
        {
            if (Application.isEditor == false)// just for performance
            {
                Debug.unityLogger.logEnabled = false;
            }
        }
        public void LoadScene(string name)
        {
            SceneManager.LoadScene(name);
        }
    }

}
