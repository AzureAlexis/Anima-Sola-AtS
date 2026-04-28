using System.Collections.Generic;
using UnityEngine;

public static class SkillManager
{    
    public static List<Skill> skills;
    public static void Start()
    {
        CreateSkillList();
    }

    static void CreateSkillList()
    {
        skills = new List<Skill>();

        // Basic skills
        skills.Add(new Skill() { name = "Cut",
            desc = "A quick cut that deals light slashing damage to one adjacent target. Not powerful, but very fast",
            damageType = "Slashing",
            power = 0.3,
            cost = 3
        });
        skills.Add(new Skill() { name = "Bash",
            desc = "Slam an enemy with your weapon to deal light blunt damage.",
            damageType = "Blunt",
            power = 0.7,
            accuracy = 0.9,
            cost = 5
        });
        skills.Add(new Skill() { name = "Crush",
            desc = "A powerful strike that deals medium blunt damage to one adjacent target. Has a small chance to stun the target.",
            damageType = "Blunt",
            power = 1.2,
            accuracy = 0.8,
            inflictAcc = 0.2,
            inflictStatus = "Stun",
            cost = 8
        });

        // Knife skills
        skills.Add(new Skill() { name = "Stab",
            desc = "Deals light piercing damage to one adjacent target. Has a chance to hit the heart of humanoid enemies and deal heavy damage instead.",
            damageType = "Piercing",
            power = 0.6,
            cost = 4
        });
        skills.Add(new Skill() { name = "Hack Away",
            desc = "Slash rapidly, dealing multiple hits with moderate accuracy in a small area in front of you.",
            damageType = "Slashing",
            power = 0.3,
            accuracy = 0.7,
            minHits = 3,
            maxHits = 3,
            cost = 6
        });
        skills.Add(new Skill() { name = "Critical Slash",
            desc = "A powerful, arcing attack that deals medium slashing damage to one adjacent target. Has a chance to deal heavy damage instead.",
            damageType = "Slashing",
            power = 0.6,
            cost = 7
        });

        // Scalpel skills
        skills.Add(new Skill() { name = "Scalpel Ballet",
            desc = "A flurry of precise strikes. Deals tiny slashing damage 4-6 times to one adjacent target, each hit having a tiny chance to inflict bleeding.",
            damageType = "Slashing",
            power = 0.15,
            accuracy = 1.0,
            inflictAcc = 0.15,
            inflictStatus = "Bleed",
            cost = 8
        });
        skills.Add(new Skill() { name = "Inscision",
            desc = "Make a precise cut to open the enemy's wounds. Deals light piercing damage to one adjacent target with a high chance to inflict bleeding.",
            damageType = "Piercing",
            accuracy = 1.0,
            power = 0.4,
            inflictAcc = 0.5,
            inflictStatus = "Bleed",
            cost = 4
        });
        skills.Add(new Skill() { name = "Lacerate",
            desc = "Deals tiny slashing damage to one adjacent target with a small chance to inflict bleeding. If the target is already bleeding, scores an additional hit.",
            damageType = "Slashing",
            power = 0.25,
            accuracy = 1.0,
            inflictAcc = 0.2,
            inflictStatus = "Bleed",
            specialCondition = () => { return BattleManager.currentEnemy.StatusStacks("Bleed") > 0; },
            specialEffect = () => { BattleManager.currentEnemy.TakeDamage((int)(BattleManager.currentCharacter.TotalPower() * 0.2 * Random.Range(0.8f, 1.2f)), "Slashing"); },
            cost = 3
        });

        // Dagger/Otherworldly skills (Locked to Amber)
        skills.Add(new Skill() { name = "Weak Stab",
            desc = "A desperate strike that deals light piercing damage to one adjacent target. Not very powerful or accurate.",
            damageType = "Piercing",
            power = 0.3,
            accuracy = 0.7,
            cost = 3
        });
        skills.Add(new Skill() { name = "Void Ice Sign 'Collapse'",
            desc = "Make the very space around an enemy colapse on itself. Deals lethal damage to one target at range and inflicts bleeding.",
            damageType = "Magic",
            power = 2.5,
            recoil = 14,
            cost = 14,
            inflictStatus = "Bleed",
            inflictAcc = 0.8
        });
        skills.Add(new Skill() { name = "Void Spirit Sign 'Open Universe'",
            desc = "Summon slivers of the void to strike many enemies. Deals moderate damage to all enemies in an area.",
            damageType = "Magic",
            power = 0.8,
            cost = 10,
            recoil = 10
        });
        skills.Add(new Skill() { name = "Void Wind Sign 'Distant Convergence'",
            desc = "Collapse a wide area of space into a single point. Deals heavy damage to all enemies in a line, and has a chance to inflict stun.",
            damageType = "Magic",
            power = 1.2,
            cost = 12,
            recoil = 12,
            inflictAcc = 0.3,
            inflictStatus = "Stun"
        });

        // Aurora skills
        skills.Add(new Skill(){name = "Calm Down",
            desc = "Take a moment to collect yourself and focus. Restores a small amount of health and cures fatigue.",
            type = 1,
            cureStatus = "Fatigue",
            cost = 5
        });
        skills.Add(new Skill(){name = "Drop Guard",
            desc = "Plead with the enemy to stop fighting. Has a chance to lower the enemy's power and defense and makes them more susceptible to persuasion. Effectiveness based on charm.",
            inflictAcc = 0.05,
            inflictAccStat = "Charm",
            inflictStatus = "Drop Guard",
            cost = 5
        });

         // Vivian Skills
        skills.Add(new Skill(){name = "Field Cure",
            desc = "Try to quickly dignose and cure various alements. Has a chance to cure most status effects. If the target has multiple status effects, cures one at random (Excluding Fatigue)",
            type = 1,
            cureStatus = "Random",
            cureAcc = 0.6,
            cost = 8
        
        });
        skills.Add(new Skill(){name = "First Aid",
            desc = "Patch up new wounds before they become worse. Heals a small amount of HP to one targert. Only works immediatly after taking damage."
        });

        // Moves
        skills.Add(new Skill(){name = "Dash",
            desc = "Quickly move up to 5 tiles. Grants momentum for your next attack this turn.",
            type = 2,
            distance = 5,
            cost = 4,
            inflictStatus = "Momentum"
        });
        skills.Add(new Skill(){name = "Reposition",
            desc = "Move to any tile.",
            type = 2,
            distance = -1,
            cost = 15
        });
        skills.Add(new Skill(){name = "Tactical Tweak",
            desc = "Move 1 tile without provoking opportunity attacks. Grants slight momentum.",
            type = 2,
            distance = 1,
            cost = 2,
        });



            // Tactics
            skills.Add(new Skill(){name = "Guard",
            desc = "Prepare yourself for oncoming attacks, reducing damage taken until your next turn. Cannot be used if this unit attacked this turn, and this unit cannot attack after guarding.",
            cost = 4,
            target = 2,
            inflictStatus = "Guard"
        });
        skills.Add(new Skill(){name = "Equip",
            desc = "Switch any amount of equipment.",
            cost = 5,
            special = "Equip"
        });
        skills.Add(new Skill(){name = "Run",
            desc = "Try to escape from combat - there's no shame in running away. Chance to succeed is based on number of friendly units in an enemy's zone of control. Can only be used by one unit once per turn.",
            target = 2,
            cost = 0,
            special = "Run"
        });

        // Special skills, only used in story battles
        skills.Add(new Skill()
        {
            name = "Rush",
            desc = "Attack many times.",
            debugDesc = "Effectively ends the fight, since this deals far more damage than any enemy can take. Only used in some story battles.",
            damageType = "Untyped",
            cost = 25,
            minHits = 50,
            maxHits = 50,
            power = 0.5
        });
    }

    public static Skill GetSkillByName(string name)
    {
        foreach (Skill skill in skills)
        {
            if (skill.name == name) return skill;
        }
        return null;
    }

    public static List<Skill> GetSkillsetByNames(string[] names)
    {
        List<Skill> skillset = new List<Skill>();
        for (int i = 0; i < names.Length; i++)
        {
            skillset.Add(GetSkillByName(names[i]));
        }
        return skillset;
    }

    public static List<Skill> Tactics()
    {
        return GetSkillsetByNames(new string[] {"Guard", "Equip", "Run"});
    }

    public static List<Skill> SocialSkills()
    {
        // Implement social skills later
        return null;
    }
}
