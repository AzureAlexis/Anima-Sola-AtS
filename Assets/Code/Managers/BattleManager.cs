using System.Collections.Generic;  
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public static class BattleManager
{
    static bool inBattle = false;
    static string state; // CharacterSelect, TopMenu, SkillSelect, TargetSelect, EnemyTurn
    static int actionPoints = 0;

    public static Enemy currentEnemy;
    public static Character currentCharacter;
    public static Skill currentSkill;

    // Targeting variables.
    static string lookingForTarget; // "Enemy", "Character", or "Tile"
    static Character targetCharacter;
    static Enemy targetEnemy;
    static Vector2 targetTile;

    
    public static List<string> topCommands = new List<string> {"Fight", "Tech", "Talk", "Move", "Item", "Tactic"};
    public static List<Skill> subCommands = new List<Skill> {};

    static int index = 0;

    public static void Start()
    {
        StartBattle();
    }
    public static void Update()
    {
        if(inBattle && Interpreter.Ready())
        {
            UpdateIndex();
            switch (state)
            {
                case "CharacterSelect":
                    UpdateCharacterSelect();
                    break;
                case "TopMenu":
                    UpdateTopMenu();
                    break;
                case "SkillSelect":
                    UpdateSkillSelect();
                    break;
                case "TargetSelect":
                    UpdateTargetSelect();
                    break;
            }
        }
    }
    public static void StartBattle()
    {
        inBattle = true;
        actionPoints = 0;
        state = "CharacterSelect";
        BattleUi.Instance.StartBattle();
    }
    // Functions relating to player control during their turn.
    // This was originally part of BattleUi, but was moved here to seperate animation from logic.
    #region PlayerControl
    static void UpdateIndex()
    {
        if (state != "TopMenu" && state != "SkillSelect")
        {
            index = 0;
            return;
        }

        int maxIndex = 0;

        if (state == "TopMenu")
            maxIndex = topCommands.Count - 1;
        else if (state == "SkillSelect")
            maxIndex = subCommands.Count - 1;

        if (Input.GetKeyDown(KeyCode.DownArrow))
            index++;
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            index--;

        if (index > maxIndex)
            index = 0;
        else if (index < 0)
            index = maxIndex;
    }
    static void StartCharacterSelect()
    {
        state = "CharacterSelect";
        lookingForTarget = "Character";
        BattleCursor.Instance.Activate();
        targetCharacter = null;
    }

    static void UpdateCharacterSelect()
    {
        if(lookingForTarget != "Character")
            StartCharacterSelect();

        if(targetCharacter != null)
        {
            currentCharacter = targetCharacter;
            targetCharacter = null;
            StartTopMenu();
        }
    }

    static void StartTopMenu()
    {
        state = "TopMenu";
    }
    
    static void UpdateTopMenu()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            BuildSkillList();
            StartSkillSelect();
        }
        else if (Input.GetKeyDown (KeyCode.X))
        {
            StartCharacterSelect();
        }
    }
    static void StartSkillSelect()
    {
        state = "SkillSelect";
    }
    static void UpdateSkillSelect()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (subCommands.Count == 0) return;
            Skill selectedSkill = subCommands[index];
            if (selectedSkill.type == 0)
                StartTargetSelect("Enemy");
            else if (selectedSkill.type == 1)
                StartTargetSelect("Character");
            else if (selectedSkill.type == 2)
                TrySkill(selectedSkill, currentCharacter, currentCharacter);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            StartTopMenu();
        }
    }
    static void StartTargetSelect(string targetType)
    {
        state = "TargetSelect";
        lookingForTarget = targetType;
        targetEnemy = null;
        targetCharacter = null;
        targetTile = Vector2.zero;
    }
    static void UpdateTargetSelect()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (lookingForTarget == "Enemy" && targetEnemy != null)
            {
                TrySkill(currentSkill, currentCharacter, targetEnemy);
                StartSkillSelect();
            }
            else if (lookingForTarget == "Character" && targetCharacter != null)
            {
                TrySkill(currentSkill, currentCharacter, targetCharacter);
                StartSkillSelect();
            }
            else if (lookingForTarget == "Tile" && targetTile != Vector2.zero)
            {
                StartSkillSelect();
            }
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            StartSkillSelect();
        }
    }
    #endregion
    static void EndBattle()
    {
        inBattle = false;
        actionPoints = 0;
        foreach (Character chara in PartyManager.party)
        {
            chara.CureStatus("Bleed");
            chara.CureStatus("Stun");
            chara.CureStatus("Fatigue");
        }
    }
    static void BuildSkillList()
    {
        subCommands.Clear();

        if (currentCharacter == null)
            currentCharacter = PartyManager.Get("Aurora");
        switch (index)
        {
            case 0:
                subCommands.AddRange(currentCharacter.WeaponSkills());
                break;
            case 1:
                subCommands.AddRange(currentCharacter.Skills());
                break;
            case 2:
                subCommands.AddRange(SkillManager.SocialSkills());
                break;
            case 3:
                subCommands.AddRange(currentCharacter.Moves());
                break;
            case 4:
                subCommands.AddRange(currentCharacter.Items());
                break;
            case 5:
                subCommands.AddRange(SkillManager.Tactics());
                break;
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
        }
    }
    // Functions related to performing actions (skills)
    #region
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
            if (skill.type == 0)
            {
                if (skill.accuracy < Random.Range(0f, 1f)) continue;   // Miss

                int attackPower = (int)(user.TotalPower() * skill.power * Random.Range(0.8f, 1.2f));
                target.TakeDamage(attackPower, skill.damageType);
            }
            else if (skill.type == 1)
            {
                target.hp += (int)skill.power;
                if (target.hp > target.mhp) target.hp = target.mhp;
            }

            if (TryInflictStatus(skill, user))
                target.InflictStatus(skill.inflictStatus);
            if (TryCureStatus(skill, user))
                target.CureStatus(skill.cureStatus);
        }
    }

    static bool TryInflictStatus(Skill skill, Unit user)
    {
        if(skill.inflictAccStat != null)
        {
            if (user.TotalStat(skill.inflictAccStat) * skill.inflictAcc >= Random.Range(0f, 1f))
                return true;
        } 
        else
        {
            if (skill.inflictAcc >= Random.Range(0f, 1f))
                return true;
        }
        return false;
    }
    static bool TryCureStatus(Skill skill, Unit user)
    {
        if (skill.cureAccStat != null)
        {
            if (user.TotalStat(skill.cureAccStat) * skill.cureAcc >= Random.Range(0f, 1f))
                return true;
        }
        else
        {
            if (skill.cureAcc >= Random.Range(0f, 1f))
                return true;
        }
        return false;
    }
    #endregion
    public static void Select(Character character)
    {
        targetCharacter = character;
    }
    public static void Select(Enemy enemy)
    {
        targetEnemy = enemy;
    }
    public static void Select(Vector2 tile)
    {
        targetTile = tile;
    }

    public static bool InBattle()
    {
        return inBattle;
    }
    public static string State()
    {
        return state;
    }
    public static int Index()
    {
        return index;
    }
}
