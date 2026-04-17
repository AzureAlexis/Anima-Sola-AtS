using System.Collections.Generic;
using UnityEngine;

public static class EquipmentManager
{
    public static List<Weapon> weapons;
    public static List<Armor> armors;
    public static List<Charm> charms;

    public static List<Weapon> heldWeapons;
    public static List<Armor> heldArmors;
    public static List<Charm> heldCharms;

    public static void Start()
    {
        CreateWeaponList();
        CreateArmorList();
        CreateCharmList();
    }

    // Create database of equipment
    static void CreateWeaponList()
    {
        weapons = new List<Weapon>
        {
            new Weapon(){name = "Knife",
                desc = "A simple pocket knife. You had this since the beginning, and it's getting a little dull. Used for quick, low power attacks.",
                power = 3,
                skills = SkillManager.GetSkillsetByNames(new string[] { "Cut", "Stab", "Backstab", "Critical Slash" })
            },
            new Weapon(){name = "Scalpel",
                desc = "A surgical scalpel. Deals quick, precise hits that can open wounds and inflict bleeding.",
                power = 1,
                skills = SkillManager.GetSkillsetByNames(new string[] { "Cut", "Lacerate", "Inscision", "Scalpel Ballet" })
            }
        };
    }
    static void CreateArmorList()
    {
        armors = new List<Armor>
        {
            new Armor(){name = "Aurora's Jacket",
                desc = "It's been through a lot over the years, but still offers some protection from the elements. Means a lot to Aurora.",
                power = 1,
                stamina = 1,
                agility = 1,
                intelligence = 1,
                charisma = 1,
                slashingDef = 0.05,
                piercingDef = 0.05,
                bluntDef = 0.05,
                magicDef = 0.05
            },
            new Armor(){name = "Cotton Overshirt",
                desc = "A light grey overshirt. Offers minimal protection.",
                slashingDef = 0.05,
                piercingDef = 0.03,
                bluntDef = 0.02,
                magicDef = 0.01
            },
            new Armor(){name = "Shining Undershirt",
                desc = "A strange, light grey piece of armor that's very flexible, yet durable. It's nothing like you've seen before.",
                stamina = 2,
                slashingDef = 0.4,
                piercingDef = 0.4,
                bluntDef = 0.4,
                magicDef = 0.3
            }
        };
    }
    static void CreateCharmList()
    {
        charms = new List<Charm>
        {
            new Charm(){name = "Woven Bracelet",
                desc = "A bracelet made by Aurora's friend, Isa. It reminds her of home.",
                power = 1,
                stamina = 1,
                intelligence = 1,
                charisma = 1,
                agility = 1,
                cost = 1
            },
            new Charm(){name = "Bus Ticket",
                desc = "Vivian's bus ticket that should have been her way to start a new life. She didn't have the chance to use it before everything happened.",
                intelligence = 2,
                agility = 1,
                cost = 1
            }
        };
    }

    // Add equipment to inventory. Mainly used for looting.
    public static void AddHeldWeapon(string name)
    {
        Weapon w = GetWeapon(name);
        if(w != null) heldWeapons.Add(w);
    }
    public static void AddHeldArmor(string name)
    {
        Armor a = GetArmor(name);
        if(a != null) heldArmors.Add(a);
    }
    public static void AddHeldCharm(string name)
    {
        Charm c = GetCharm(name);
        if(c != null) heldCharms.Add(c);
    }
    
    // Remove equipment from inventory. Mainly used for equipping.
    public static Weapon RemoveHeldWeapon(string name)
    {
        foreach(Weapon w in heldWeapons)
        {
            if(w.name == name)
            {
                heldWeapons.Remove(w);
                return w;
            }
        }
        return null;
    }
    public static Armor RemoveHeldArmor(string name)
    {
        foreach(Armor a in heldArmors)
        {
            if(a.name == name)
            {
                heldArmors.Remove(a);
                return a;
            }
        }
        return null;
    }
    public static Charm RemoveHeldCharm(string name)
    {
        foreach(Charm c in heldCharms)
        {
            if(c.name == name)
            {
                heldCharms.Remove(c);
                return c;
            }
        }
        return null;
    }

    // Getters for database
    public static Weapon GetWeapon(string name)
    {
        foreach(Weapon w in weapons)
            if(w.name == name) return w;
        return null;
    }
    public static Armor GetArmor(string name)
    {
        
        foreach(Armor a in armors)
            if(a.name == name) return a;
        return null;
    }
    public static Charm GetCharm(string name)
    {
        foreach(Charm c in charms)
            if(c.name == name) return c;
        return null;
    }
}
