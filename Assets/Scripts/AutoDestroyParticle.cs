using UnityEngine;

public class AutoDestroyParticle : MonoBehaviour
{
    public float destroyDelay = 1f;

    void Start()
    {
        // Elpusztítja ezt a GameObjectet a megadott másodperc múlva, 
        // hogy ne szemetelje tele a memóriát klónokkal.
        Destroy(gameObject, destroyDelay);
    }
}
