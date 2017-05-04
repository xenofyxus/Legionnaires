using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Interface.SettingsMenu.MainMenu
{
    public class MainMenuBehaviour : MonoBehaviour
    {

        public void Return()
        {
            SceneManager.LoadScene(0);
        }
    }
}