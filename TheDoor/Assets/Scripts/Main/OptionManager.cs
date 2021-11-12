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
     //   masterSlider.value = PlayerPrefs.GetInt("masterAudioMixer");
     //   bgmSlider.value = PlayerPrefs.GetInt("bgmAudioMixer");
     //   sfxSlider.value = PlayerPrefs.GetInt("sfxAudioMixer");
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
