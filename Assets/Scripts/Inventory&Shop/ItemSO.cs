using UnityEngine;

[CreateAssetMenu(fileName = "New Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite icon;

    public bool isGold;
    public int stackSize = 3;

    [Header("Stats")]
    public float currentHealth;
    public float maxHealth;
    public float speed;
    public float maxspeed;
    public int damage;

    [Header("Temporary items")]
    public float duration;
}
