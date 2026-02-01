using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SkillSlot : MonoBehaviour
{
    public SkillSO skillSO;

    public int currentLevel;
    public Image skillIcon;
    public TMP_Text skillLevelText;
    public Button skillButton;

    public static event Action<SkillSlot> OnAbilityPointSpent;

    private void OnValidate()
    {
        if (skillSO != null && skillLevelText != null && skillButton != null)
        {
            UpdateUI();
        }
    }

    public void TryUpgradeSkill()
    {
        if (currentLevel < skillSO.maxLevel)
        {
            currentLevel++;
            OnAbilityPointSpent?.Invoke(this);
            UpdateUI();
        }
    }


    private void UpdateUI()
    {
        skillButton.interactable = true;
        skillIcon.sprite = skillSO.skillIcon;
        skillLevelText.text = currentLevel.ToString() + "/" + skillSO.maxLevel.ToString();
    }
}
