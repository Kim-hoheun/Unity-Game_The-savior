using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEvent : MonoBehaviour
{
    public AudioClip MainMenuSound;
    public bool MainMenu;
    public AudioSource MenuSound;
    public AudioClip GamePlaySound;
    public AudioClip MoveSound;
    public AudioClip DevilSound;
    public AudioClip BoxSound;
    public AudioClip SpineSound;
    public AudioClip KeySound;
    public AudioClip ClearSound;
    public AudioClip BossDileSound;
    private void Start()
    {
        if (MainMenu)
        {
            PlaySound("Mainmenu",MenuSound);
            MenuSound.Play();
        }
    }
    public void PlaySound(string action, AudioSource audioSource)
    {
        switch (action)
        {
            case "Mainmenu":
                audioSource.clip = MainMenuSound;
                break;
            case"GamePlay":
                audioSource.clip = GamePlaySound;
                break;
            case "Move":
                audioSource.clip = MoveSound;
                break;
            case "Devil":
                audioSource.clip = DevilSound;
                break;
            case "Box":
                audioSource.clip = BoxSound;
                break;
            case "Spine":
                audioSource.clip = SpineSound;
                break;
            case "Key":
                audioSource.clip = KeySound;
                break;
            case "Clear":
                audioSource.clip = ClearSound;
                break;
            case "Boss":
                audioSource.clip = BossDileSound;
                break;
        }
    }
}
