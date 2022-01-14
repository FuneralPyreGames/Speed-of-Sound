using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MainMenuAudio : MonoBehaviour
{
    public AudioSource mainMenuAudio;
    public AudioClip mainMenuSong;
    public AudioClip connectionSoundEffect;
    public AudioClip buttonSoundEffect;
    public AudioClip joinedRoomSoundEffect;

    private void Awake()
    {
        PlayMainMenuSong();
    }

    public void PlayMainMenuSong()
    {
        mainMenuAudio.clip = mainMenuSong;
        mainMenuAudio.Play();
    }
    public void PlayConnectionSoundEffect()
    {
        mainMenuAudio.PlayOneShot(connectionSoundEffect);
    }

    public void PlayButtonSoundEffect()
    {
        mainMenuAudio.PlayOneShot(buttonSoundEffect);
    }

    public void PlayJoinRoomSoundEffect()
    {
        mainMenuAudio.PlayOneShot(joinedRoomSoundEffect);
    }
}