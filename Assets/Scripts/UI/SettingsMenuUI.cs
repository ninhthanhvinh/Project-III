using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace RPG.UI
{
    public class SettingsMenuUI : MonoBehaviour
    {
        private AudioMixer musicMixer;
        private AudioMixer sfxMixer;
        private void Start()
        {
            musicMixer = SoundManager.instance.MusicMixer;
            sfxMixer = SoundManager.instance.SFXMixer;
        }
        public void ChangeMusicVolumn(float value)
        {
            musicMixer.SetFloat("Volumn", Mathf.Log10(value) * 20 + 20);
        }

        public void ChangeSFXVolumn(float value)
        {
            sfxMixer.SetFloat("Volumn", Mathf.Log10(value) * 20 + 20);
        }

        public void ChangeResolution()
        {

        }

        public void GoToMainMenu()
        {
            FindObjectOfType<GameManager>().LoadScene(0);
        }

        public void ChangeMusicState(bool value)
        {
            if (value)
            {
                musicMixer.SetFloat("Volumn", 0);
            }
            else
            {
                   musicMixer.SetFloat("Volumn", -80);
            }
        }
        public void ChangeSFXState(bool value)
        {
            if (value)
            {
                sfxMixer.SetFloat("Volumn", 0);
            }
            else
            {
                sfxMixer.SetFloat("Volumn", -80);
            }
        }

    }

}