using UnityEngine;
using System.Collections.Generic;  

public static class BattleManager
{
    static bool inBattle = false;
    static int actionPoints = 0;

    static void StartBattle()
    {
        inBattle = true;
        actionPoints = 0;
    }

    static void EndBattle()
    {
        inBattle = false;
        actionPoints = 0;
        foreach(Character chara in PartyManager.party)
        {
            chara.CureStatus("Bleed");
            chara.CureStatus("Stun");
            chara.CureStatus("Fatigue");
        }
    }

    static void Run()
    {
        float chance = 0.8f;
        foreach(Character chara in PartyManager.party)
            if(chara.InControlZone()) chance -= 0.2f;
        if(chance < Random.Range(0f, 1f)) EndBattle();   // Successfully escaped, end the battle.
    }

    static void StartPlayerTurn()
    {
        if (PartyManager.party.Count == 1) actionPoints += 2;
        else if (PartyManager.party.Count == 2) actionPoints += 1;
        foreach(Character chara in PartyManager.party)
        {
            actionPoints += chara.agility;
            chara.DecreaseStatus("Fatigue", 1);
            chara.DecreaseStatus("Stun", 1);
        }
    }

    static void TrySkill(Skill skill, Unit user, Unit target)
    {
        if (actionPoints < skill.cost) return;   // Not enough action points
        actionPoints -= skill.cost;

        PerformTargetedSkill(skill, user, target);
    }
    static void PerformTargetedSkill(Skill skill, Unit user, Unit target)
    {
        int hits = Random.Range(skill.minHits, skill.maxHits);

        for (int i = 0; i < hits; i++)
        {
            // Attack behavior
            if (skill.type == 0)
            {
                if (skill.accuracy < Random.Range(0f, 1f)) continue;   // Miss

                int attackPower = (int)(user.TotalPower() * skill.power * Random.Range(0.8f, 1.2f));
                target.TakeDamage(attackPower, skill.damageType);
            }
            // Heal behavior
            else if (skill.type == 1)
            {
                target.hp += (int)skill.power;
                if (target.hp > target.mhp) target.hp = target.mhp;
            }

            // Shared status effects
            if (skill.inflictStatus != null && skill.inflictAcc > Random.Range(0f, 1f))
                target.InflictStatus(skill.inflictStatus);

            target.CureStatus(skill.cureStatus);
        }
    }
}
