using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Netrunner
{
    public class LevelFinish : PlayerContainedTracker
    {
        public string NextScene;
        public int CurrentId;
        public override void PlayerIn()
        {
            playercnt++;
            if (playercnt == 2)
            {
                SaveManager.Current.CompletedLevel = SaveManager.Current.CompletedLevel < CurrentId ? CurrentId : SaveManager.Current.CompletedLevel;
                SaveManager.Save();
                SceneManager.LoadScene(NextScene);
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}

