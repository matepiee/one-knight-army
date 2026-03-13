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
        graphicsDropdown.value = QualitySettings.GetQualityLevel();
        graphicsDropdown.RefreshShownValue();

        // Értékek betöltése PlayerPrefs-bõl (ha még nincs mentve, az alapértelmezett 0.75f)
        masterVol.value = PlayerPrefs.GetFloat("MasterVol", 0.75f);
        musicVol.value = PlayerPrefs.GetFloat("MusicVol", 0.75f);
        sfxVol.value = PlayerPrefs.GetFloat("SFXVol", 0.75f);

        // Azonnali frissítés, hogy a Mixer is tudja
        UpdateVolumes();
    }

    private void UpdateVolumes()
    {
        ChangeMasterVolume();
        ChangeMusicVolume();
        ChangeSFXVolume();
    }

    public void ChangeMasterVolume()
    {
        float val = masterVol.value;
        mainAudioMixer.SetFloat("MasterVol", Mathf.Log10(val) * 20);
        PlayerPrefs.SetFloat("MasterVol", val); // Mentés
    }

    public void ChangeMusicVolume()
    {
        float val = musicVol.value;
        mainAudioMixer.SetFloat("MusicVol", Mathf.Log10(val) * 20);
        PlayerPrefs.SetFloat("MusicVol", val); // Mentés
    }

    public void ChangeSFXVolume()
    {
        float val = sfxVol.value;
        mainAudioMixer.SetFloat("SFXVol", Mathf.Log10(val) * 20);
        PlayerPrefs.SetFloat("SFXVol", val); // Mentés
    }

    public void ChangeGraphicsQuality()
    {
        QualitySettings.SetQualityLevel(graphicsDropdown.value);
    }
}