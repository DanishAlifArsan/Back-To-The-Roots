using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void Setting() {
        //ke menu setting
    }

    public void Quit() {
        Application.Quit();
    }
}
