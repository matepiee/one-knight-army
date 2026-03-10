using System;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public Player_ChangeEquipment bow;

    private void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += HandleAbilityPointSpent;
    }
    private void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent += HandleAbilityPointSpent;
    }

    private void HandleAbilityPointSpent(SkillSlot slot)
    {
        string skillName = slot.skillSO.skillName;

        switch (skillName)
        {
            case "Max Health Boost":
                StatsManager.Instance.UpdateMaxHealth(10);
                break;
            case "Attack Damage Boost":
                StatsManager.Instance.UpdateAttackDamage(5);
                break;
           case "Speed Boost":
                StatsManager.Instance.UpdateSpeed(0.2f);
                break;
            case "Arrow Buff":
                StatsManager.Instance.UpdateArrow(5,0.1f);
                break;
            case "Gold Boost":
                StatsManager.Instance.UpdateGoldGain(1);
                break;
            case "Archery Unlock":
                bow.enabled = true;
                break;



            default:
                Debug.Log("Unknown skill: " + skillName);
                break;
        }
    }
}
