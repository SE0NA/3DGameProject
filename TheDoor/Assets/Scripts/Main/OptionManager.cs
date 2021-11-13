using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionManager : MonoBehaviour
{
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;

    public GameObject[] activeFalseObejcts;
    public GameObject optionPanel;

    public AudioMixer myAudio;

    public void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterAudioMixer");
        bgmSlider.value = PlayerPrefs.GetFloat("bgmAudioMixer");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxAudioMixer");
    }

    public void ControlSlider(int sliderNum)
    {
        float sound;
        switch (sliderNum)
        {
            case 0: // master
                sound = masterSlider.value;
                if (sound == -20f) myAudio.SetFloat("master", -80);
                else               myAudio.SetFloat("master", sound);
                PlayerPrefs.SetFloat("masterAudioMixer", sound);
                break;

            case 1: // bgm
                sound = bgmSlider.value;
                if (sound == -20f) myAudio.SetFloat("bgm", -80);
                else myAudio.SetFloat("bgm", sound);
                PlayerPrefs.SetFloat("bgmAudioMixer", sound);
                break;

            case 2: // sfx
                sound = sfxSlider.value;
                if (sound == -20f) myAudio.SetFloat("sfx", -80);
                else myAudio.SetFloat("sfx", sound);
                PlayerPrefs.SetFloat("sfxAudioMixer", sound);
                break;
        }
    }

    public void ClickBackBtn()
    {
        for(int i=0;i< activeFalseObejcts.Length; i++)
        {
            activeFalseObejcts[i].SetActive(true);
        }
        optionPanel.SetActive(false);
    }
}
