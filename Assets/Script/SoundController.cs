using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoundController : MonoBehaviour
{
    public enum TypeSound
    {
        Sound,
        Music
    }

    [System.Serializable]
    public class PlayAudio
    {
        public string Name;
        public AudioClip Audio;
    }


    public static SoundController instance;
    public AudioSource music;
    public AudioSource sound;

    public PlayAudio[] playAudios;
    public Dictionary<string, AudioClip> Dic_audioClip = new();

    [Header("Sound BG")]
    public AudioClip[] music_BG;
    public AudioClip s_Click;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }


    }
    private void Start()
    {
        for (int i = 0; i < playAudios.Length; i++)
        {
            Dic_audioClip.Add(playAudios[i].Name, playAudios[i].Audio);
        }
    }
    public void ChangeSettingMusic()
    {
        music.mute = DataManager.instance.saveData.offmusic;
    }
    public void ChangeSettingSound()
    {
        sound.mute = DataManager.instance.saveData.offsound;
    }
    public void MuteAllMusic()
    {
        music.mute = true;
    }
    public void MuteAllSound()
    {
        sound.mute = true;
    }
    
    public void PlaySoundClick()
    {
        sound.PlayOneShot(s_Click);
        
    }
    
    public void PlaySound(string NameAudioClip)
    {

        sound.PlayOneShot(Dic_audioClip[NameAudioClip]);
    }



    public void PlayMusic(string NameAudioClip)
    {
        music.clip = Dic_audioClip[NameAudioClip];
        music.Play();
    }
    public void OffMusic()
    {
        music.Stop();
    }

    
    public void PlayMusic(AudioClip music_name)
    {
        music.clip = music_name;
        music.Play();
    }

    public void SoundClickButton()
    {
        sound.PlayOneShot(s_Click);
        if (DataManager.instance.saveData.session == 1)
        {

        }
    }
    public void PlayMusicOnPlay()
    {
        if (music.clip != music_BG[0])
        {
            music.clip = music_BG[0];
            music.Play();
        }
    }

}
