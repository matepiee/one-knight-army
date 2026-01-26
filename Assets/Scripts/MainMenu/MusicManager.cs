using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    void Awake()
    {
        // Megnézzük, létezik-e már egy ilyen objektum
        if (instance == null)
        {
            instance = this;
            // Megmondjuk a Unity-nek, hogy ne törölje le váltáskor
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Ha már van egy (pl. visszaléptünk a menübe), az újat töröljük
            Destroy(gameObject);
        }
    }
}