using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;

public class MenuVolumeController : MonoBehaviour
{
    public Slider bgmVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider uiVolumeSlider;

    public bool volumeOptionMenuOpenClosed;
    public GameObject volumeOptionUI;

    void Start()
    {
        bgmVolumeSlider.onValueChanged.AddListener(OnBGMSliderValueChanged);
        sfxVolumeSlider.onValueChanged.AddListener(OnSFXSliderValueChanged);
        uiVolumeSlider.onValueChanged.AddListener(OnUISliderValueChanged);
        volumeOptionUI.SetActive(volumeOptionMenuOpenClosed);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            volumeOptionMenuOpenClosed = !volumeOptionMenuOpenClosed;
            volumeOptionUI.SetActive(volumeOptionMenuOpenClosed);
        }
    }

    void OnBGMSliderValueChanged(float value)
    {
        AudioManager.main.VolumeByType(AudioType.BGM, value);
    }
    
    void OnSFXSliderValueChanged(float value)
    {
        AudioManager.main.VolumeByType(AudioType.SFX, value);
    }

    void OnUISliderValueChanged(float value)
    {
        AudioManager.main.VolumeByType(AudioType.UI, value);
    }
}
