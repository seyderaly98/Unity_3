﻿using System;
using System.Collections;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

internal sealed class GameController : MonoBehaviour
{
     public enum Screen
     {
        PauseMenu,
        ButtonsScreen,
        GameResultMenu,
        SoundSettingsMenu
     }

     [SerializeField] private CanvasGroup ButtonsScreen;
     [SerializeField] private CanvasGroup PauseManu;
     [SerializeField] private CanvasGroup GameResultManu;
     [SerializeField] private CanvasGroup SoundSettingsMenu;
     [SerializeField] private TextMeshProUGUI _textGameReult;
     [SerializeField] private TextMeshProUGUI _throwBoneTextMeshProUGUI;
    public Character[] playerCharacter;
    public Character[] enemyCharacter;
    Character currentTarget;
    bool waitingForInput;

    private AudioSource click;




    // private void Awake()
    // {
    //     Slider first = SoundSettingMenu.instance.GetComponentsInChildren<Slider>().FirstOrDefault(s => s.name == "Background");
    //
    //     if (first is null) return;
    //     var t = first.GetComponent<AudioSource>();
    //     t.clip = audioBackground;
    //     t.Play();
    // }

    Character FirstAliveCharacter(Character[] characters)
    {
        return characters.FirstOrDefault(character => !character.IsDead());
    }

    
    void PlayerWon()
    {
        Debug.Log("Player won.");
    }
    void PlayerLost()
    {
        Debug.Log("Player lost.");
    }
    bool CheckEndGame()
    {
        if (FirstAliveCharacter(playerCharacter) == null) {
            PlayerLost();
            return true;
        }

        if (FirstAliveCharacter(enemyCharacter) == null) {
            PlayerWon();
            return true;
        }

        return false;
    }

    [ContextMenu("Player Attack")]
    public void PlayerAttack()
    {
        waitingForInput = false;
    }

    [ContextMenu("Next Target")]
    void NextTarget()
    {
        int index = Array.IndexOf(enemyCharacter, currentTarget);
        for (int i = 1; i < enemyCharacter.Length; i++) {
            int next = (index + i) % enemyCharacter.Length;
            if (!enemyCharacter[next].IsDead()) {
                currentTarget.targetIndicator.gameObject.SetActive(false);
                currentTarget = enemyCharacter[next];
                currentTarget.targetIndicator.gameObject.SetActive(true);
                return;
            }
        }
    }

    IEnumerator GameLoop()
    {
        yield return null;
        while (!CheckEndGame()) {
            foreach (var player in playerCharacter)
            {
                currentTarget = FirstAliveCharacter(enemyCharacter);
                if (currentTarget == null)
                    break;

                currentTarget.targetIndicator.gameObject.SetActive(true);

                waitingForInput = true;
                while (waitingForInput)
                    yield return null;

                currentTarget.targetIndicator.gameObject.SetActive(false);

                player.target = currentTarget.transform;
                player.AttackEnemy();

                while (!player.IsIdle())
                    yield return null;

                break;
            }

            foreach (var enemy in enemyCharacter)
            {
                Character target = FirstAliveCharacter(playerCharacter);
                if (target == null)
                    break;

                enemy.target = target.transform;
                enemy.AttackEnemy();

                while (!enemy.IsIdle())
                    yield return null;

                break;
            }
        }
    }
    void Start()
    {
        StartCoroutine(GameLoop());
        SetCanvasGroup(Screen.ButtonsScreen);
        click = GetComponent<AudioSource>();
    }
    public void Pause()
    {
        click.Play();
        SetCanvasGroup(Screen.PauseMenu);
    }
    public void SoundMenu()
    {
        click.Play();
        SetCanvasGroup(Screen.SoundSettingsMenu);
    }

    public void MainMenu()
    {
        click.Play();
        SceneManager.LoadScene("MainMenu");
    }
    public void StartOver()
    {
        click.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Continue()
    {
        click.Play();
        SetCanvasGroup(Screen.ButtonsScreen);
    }
     void SetCanvasGroup(Screen screen)
    {
        CanvasGroup(ButtonsScreen, screen == Screen.ButtonsScreen);
        CanvasGroup(PauseManu, screen == Screen.PauseMenu);
        CanvasGroup(GameResultManu, screen == Screen.GameResultMenu);
        CanvasGroup(SoundSettingsMenu, screen == Screen.SoundSettingsMenu);
    }
    void CanvasGroup(CanvasGroup canvasGroup,bool value )
    {
        canvasGroup.alpha = value ? 1 : -1;
        canvasGroup.interactable = value;
        canvasGroup.blocksRaycasts = value;
    }

    public bool GameResult()
    {
        if (playerCharacter[0]._State == Character.State.Dead && enemyCharacter[0]._State == Character.State.Dead)
        {
            _textGameReult.text = "Оба умерли";
            SetCanvasGroup(Screen.GameResultMenu);
            return false;
        }
        else if (playerCharacter[0]._State == Character.State.Dead)
        {
            _textGameReult.text = "Вы пройграли";
            SetCanvasGroup(Screen.GameResultMenu);
            return false;

        }
        else if (enemyCharacter[0]._State == Character.State.Dead)
        {
            _textGameReult.text = "Вы выиграли";
            SetCanvasGroup(Screen.GameResultMenu);
            return true;
        }
        return false;
    }


    public void ThrowBone()
    {
        if (playerCharacter[0].Chance > 0)
        {
            playerCharacter[0].Chance--;
            var random = new System.Random();
            var result = random.Next(1, 4);
            if (result == 1)
            {
                playerCharacter[0].GetComponent<Health>().current++;
                _throwBoneTextMeshProUGUI.text = "+1 Health";
                StartCoroutine(TT());
            }
            else
            {
                playerCharacter[0].GetComponent<Health>().current = 1;
                _throwBoneTextMeshProUGUI.text = $"упс, осталось {playerCharacter[0].Chance} броска";
                StartCoroutine(TT());
            }
        }
        else
        {
            _throwBoneTextMeshProUGUI.text = "Вы больше не можете бросить кость";
            StartCoroutine(TT());
        }
    }

    IEnumerator TT()
    {
        yield return new WaitForSeconds(2f);
        _throwBoneTextMeshProUGUI.text = String.Empty;
    }


}