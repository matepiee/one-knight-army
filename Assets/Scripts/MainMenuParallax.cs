using UnityEngine;

public class MainMenuParallax : MonoBehaviour
{
    // Itt állíthatod be, mennyire mozogjon el az adott réteg
    public float moveModifier;

    private Vector2 startPos;

    void Start()
    {
        // Elmentjük a kép eredeti helyét
        startPos = transform.position;
    }

    void Update()
    {
        // Kiszámoljuk az egér helyzetét a képernyõ közepéhez képest
        Vector2 pOffset = new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2);

        // Elmozgatjuk a réteget a módosító értékkel
        transform.position = new Vector2(startPos.x + pOffset.x * moveModifier, startPos.y + pOffset.y * moveModifier);
    }
}