using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private GameObject settingMenu;
    [SerializeField] private AudioMixer audioMixer;
    private bool isKeyArrow;
    public bool IsKeyArrow { get {return isKeyArrow; } }
    private bool isWASD;
    public bool IsWASD { get {return isWASD;} }

    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void Setting() {
        //ke menu setting
        if (!settingMenu.activeSelf)
        {
            settingMenu.SetActive(true);
        }
    }

    public void Quit() {
        Application.Quit();
    }

    private void Start() {
        settingMenu.SetActive(false);
    }

    private void Update() {
        if (settingMenu.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            settingMenu.SetActive(false);
        }
    }

    public void VolumeSetting(float volume) {
        audioMixer.SetFloat("volume",volume);
    }

   private void MovementSetting() {
    
   }

}
