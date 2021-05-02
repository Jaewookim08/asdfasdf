using UnityEngine;
using UnityEngine.SceneManagement;

namespace _4d
{
    public class RestartAction: MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}