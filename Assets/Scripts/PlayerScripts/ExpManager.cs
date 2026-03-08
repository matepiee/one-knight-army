using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class ExpManager : MonoBehaviour
{
    public int level;
    public int currentExp;
    public int expToLevel=100;
    public float expGrowth = 1.2f;
    public Slider expSlider;
    public TMP_Text currentLvl;

    public static event Action<int> OnLevelUp;

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GainExperience(2000);
        }
    }

    private void OnEnable()
    {
        Enemy_Health.OnMonsterDefeated += GainExperience;
    }
    private void OnDisable()
    {
        Enemy_Health.OnMonsterDefeated -= GainExperience;
    }

    public void GainExperience(int amount)
    {
        currentExp += amount;
        if (currentExp>=expToLevel)
        {
            LevelUp();
        }
        UpdateUI();
    }

    private void LevelUp()
    {
        level++;
        currentExp -=expToLevel;
        expToLevel = Mathf.RoundToInt(expToLevel * expGrowth);
        OnLevelUp?.Invoke(1);
    }

    public void UpdateUI()
    {
        expSlider.maxValue = expToLevel;
        expSlider.value = currentExp;
        currentLvl.text = "Level: " + level;
    }


}

