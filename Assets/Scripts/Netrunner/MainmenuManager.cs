using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Netrunner
{
    public class MainmenuManager : MonoBehaviour
    {
        public Canvas Main, Levels;
        public List<Button> Buttons = new List<Button>();

        bool main = true;

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log(Application.persistentDataPath);
            int clev = SaveManager.Current.CompletedLevel;
            for(int i=clev+1; i<Buttons.Count; i++)
            {
                Buttons[i].interactable = false;
                //Buttons[i].GetComponent<Image>().color = Color.gray;
            }
        }
        
        public void SceneChange(int i)
        {
            if (i >= 1 && i<=5) SceneManager.LoadScene("tutorial" + i, LoadSceneMode.Single);
            else if (i >= 6 && i <= 8) SceneManager.LoadScene("map1-" + (i-5), LoadSceneMode.Single);
            else if (i >= 9 && i <= 9) SceneManager.LoadScene("stage1-2", LoadSceneMode.Single);
        }

        public void StartBtn()
        {
            Main.gameObject.SetActive(false);
            Levels.gameObject.SetActive(true);
            main = false;
        }

        public void ResetBtn()
        {
            SaveManager.Delete("savefile");
            SceneManager.LoadScene("MainMenu");
        }

        public void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if (!main)
                {
                    Main.gameObject.SetActive(true);
                    Levels.gameObject.SetActive(false);
                    main = true;
                }
            }
        }
    }

}
