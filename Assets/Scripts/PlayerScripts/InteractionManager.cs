using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private bool isMenuOpen = false;
    void Update()
    {
       
    }

    // A normál UI gombokhoz (pl. Settings megnyitása)
    public void PlayButtonSound()
    {
        AudioManager.instance.Play("Test_Sound");
    }
}