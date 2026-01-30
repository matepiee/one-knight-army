using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenuManager : MonoBehaviour
{
    public TMP_Dropdown graphicsDropdown;
    public Slider masterVol, musicVol, sfxVol;
    public AudioMixer mainAudioMixer;

    void Start()
    {
        // Grafika szinkronizálása indításkor
        graphicsDropdown.value = QualitySettings.GetQualityLevel();
        graphicsDropdown.RefreshShownValue();

        // Sliderek szinkronizálása a Mixer jelenlegi értékeivel
        // Így a pötty ott lesz, ahol a hangerõ valójában áll
        float val;
        if (mainAudioMixer.GetFloat("MasterVol", out val)) masterVol.value = val;
        if (mainAudioMixer.GetFloat("MusicVol", out val)) musicVol.value = val;
        if (mainAudioMixer.GetFloat("SFXVol", out val)) sfxVol.value = val;
    }

    // Mivel a Slider már -80 és 0 között van, csak simán átadjuk az értéket
    public void ChangeMasterVolume()
    {
        mainAudioMixer.SetFloat("MasterVol", masterVol.value);
    }

    public void ChangeMusicVolume()
    {
        mainAudioMixer.SetFloat("MusicVol", musicVol.value);
    }

    public void ChangeSFXVolume()
    {
        mainAudioMixer.SetFloat("SFXVol", sfxVol.value);
    }

    public void ChangeGraphicsQuality()
    {
        QualitySettings.SetQualityLevel(graphicsDropdown.value);
    }
}