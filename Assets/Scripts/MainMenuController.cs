using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    enum Screen
    {
        MainMenu,
        LevelsMenu,
        SettingsMenu,
        SoundSettingsMenu
    }

    [SerializeField] private CanvasGroup _levelsMenu;
    [SerializeField] private CanvasGroup _mainMenu;
    [SerializeField] private CanvasGroup _settingsMenu;
    [SerializeField] private CanvasGroup _soundSettingsMenu;
    private AudioSource clic;
    void Start()
    {
        SetCanvasGroup(Screen.MainMenu);
        clic = GetComponent<AudioSource>();
    }

    void SetCanvasGroup(Screen screen)
    {
        CanvasGroup(_levelsMenu, screen == Screen.LevelsMenu);
        CanvasGroup(_mainMenu,screen == Screen.MainMenu);
        CanvasGroup(_settingsMenu,screen == Screen.SettingsMenu);
        CanvasGroup(_soundSettingsMenu,screen == Screen.SoundSettingsMenu);
    }
    void CanvasGroup(CanvasGroup @group, bool value)
    {
        @group.alpha = value ? 1 : -1;
        @group.interactable = value;
        @group.blocksRaycasts = value;
    }

    public void Play()
    {
        clic.Play();
        SetCanvasGroup(Screen.LevelsMenu);
    }

    public void Settings()
    {
        clic.Play();
        SetCanvasGroup(Screen.SettingsMenu);
    }

    public void MainMenu()
    {
        clic.Play();
        SetCanvasGroup(Screen.MainMenu);
    }

    public void SoundSettingsMenu()
    {
        clic.Play();
        SetCanvasGroup(Screen.SoundSettingsMenu);

    }

    public void Level1()
    {
        clic.Play();
        SceneManager.LoadScene("Level-1");
    }

    public void Level2()
    {
        clic.Play();
        SceneManager.LoadScene("Level-2");
    }

}
