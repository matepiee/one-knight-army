using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class ScreenFlashEffect : MonoBehaviour
{
    private Volume volume;
    private Vignette vignette;

    [Header("Settings")]
    public float flashIntensity = 0.6f;
    public float fadeSpeed = 3f;

    void Start()
    {
        volume = GetComponent<Volume>();
        if (volume.profile.TryGet(out vignette))
        {
            vignette.intensity.value = 0f;
        }
        else
        {
            Debug.LogError("Nem található Vignette effekt a Global Volume profiljában!");
        }
    }

    public void PlayFlash()
    {
        if (vignette != null)
        {
            StopAllCoroutines();
            StartCoroutine(FlashRoutine());
        }
    }

    IEnumerator FlashRoutine()
    {
        vignette.intensity.value = flashIntensity;

        while (vignette.intensity.value > 0)
        {
            vignette.intensity.value -= Time.deltaTime * fadeSpeed;

            if (vignette.intensity.value < 0) vignette.intensity.value = 0;

            yield return null;
        }
    }
}