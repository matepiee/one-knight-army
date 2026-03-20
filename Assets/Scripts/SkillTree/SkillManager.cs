using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{

    private void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += HandleAbilityPointSpent;
    }
    private void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent -= HandleAbilityPointSpent;
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
            case "Guard Unlock":
                StatsManager.Instance.UnlockGuard();
                break;
            case "Archery Unlock":
                StatsManager.Instance.UnlockArchery();
                break;
            case "Water Magic":
                StatsManager.Instance.Heal(5);
                break;
            case "Bloodlust Magic":
                StatsManager.Instance.UnlockRage();
                break;



            default:
                Debug.Log("Unknown skill: " + skillName);
                break;
        }
    }
}
