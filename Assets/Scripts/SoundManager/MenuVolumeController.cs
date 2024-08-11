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
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            volumeOptionMenuOpenClosed = !volumeOptionMenuOpenClosed;
            volumeOptionUI.SetActive(volumeOptionMenuOpenClosed);
            if(volumeOptionMenuOpenClosed)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }else{
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

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
