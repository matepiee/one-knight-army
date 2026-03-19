using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class ScreenFlashEffect : MonoBehaviour
{
    public Volume volume;
    private Vignette vignette;

    public Volume shieldVolume;
    private Vignette shieldVignette;

    private Coroutine flashCoroutine;
    private Coroutine shieldCoroutine;

    [Header("Settings")]
    public float flashIntensity = 0.6f;
    public float fadeSpeed = 3f;

    void Start()
    {

        if (volume != null)
        {
            if (volume.profile.TryGet(out vignette))
            {
                vignette.intensity.value = 0f;
            }
            else
            {
                Debug.LogError("Nem található Vignette effekt a Damage Volume profiljában!");
            }
        }

        if (shieldVolume != null)
        {
            if (shieldVolume.profile.TryGet(out shieldVignette))
            {
                shieldVignette.intensity.value = 0f;
            }
            else
            {
                Debug.LogError("Nem található Vignette effekt a Shield Volume profiljában!");
            }
        }

    }

    public void PlayFlash()
    {
        if (vignette != null)
        {
            if (flashCoroutine != null) StopCoroutine(flashCoroutine);
            flashCoroutine = StartCoroutine(FlashRoutine());
        }
    }

    public void PlayShieldFlash()
    {
        if (shieldVignette != null)
        {
            if (shieldCoroutine != null) StopCoroutine(shieldCoroutine);
            shieldCoroutine = StartCoroutine(ShieldFlashRoutine());
        }
    }

    IEnumerator FlashRoutine()
    {
        volume.priority = 100;
        vignette.intensity.value = flashIntensity;

        yield return new WaitForSeconds(0.06f);

        while (vignette.intensity.value > 0)
        {
            vignette.intensity.value -= Time.deltaTime * fadeSpeed;

            if (vignette.intensity.value < 0) vignette.intensity.value = 0;

            yield return null;
        }
        volume.priority = 1;
    }

    IEnumerator ShieldFlashRoutine()
    {
        shieldVolume.priority = 101;
        shieldVignette.intensity.value = flashIntensity;

        yield return new WaitForSeconds(0.1f);

        while (shieldVignette.intensity.value > 0)
        {
            shieldVignette.intensity.value -= Time.deltaTime * fadeSpeed;

            if (shieldVignette.intensity.value < 0) shieldVignette.intensity.value = 0;

            yield return null;
        }
        shieldVolume.priority = 1;
    }
}