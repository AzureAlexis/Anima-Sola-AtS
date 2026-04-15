using UnityEngine;

public class Status
{
    // Basic info
    public string name;
    public string desc;
    public int stacks;
    public int maxStacks = 3;    // If the status can have multiple stacks, how many it can have before it reaches max power. If 0, stacks do not increase the power of the status.
    public bool persist = false;  // If the status isn't cured at the end of battle

    // Damage over time
    public double damage = 0.0;   // Damage to add or subtract from the inflicted unit at the start of their turn. If between 0 and 1, deals damage as percent of max hp.

    // Defense stats
    public double slashingDef = 0.0;    // Defense to add or subtract from the inflicted unit against slashing attacks.
    public double piercingDef = 0.0;    // Defense to add or subtract from the inflicted unit against piercing attacks.
    public double bluntDef = 0.0;       // Defense to add or subtract from the inflicted unit against blunt attacks. If between 0 and 1, acts as a multiplier instead
    public double magicDef = 0.0;       // Defense to add or subtract from the inflicted unit against magic attacks. If between 0 and 1, acts as a multiplier instead. Does not affect psychic attacks.
    
    // Unit stats
    public double power = 0.0; // Power to add or subtract from the inflicted unit. If between 0 and 1, acts as a multiplier instead               
    public double stamina = 0.0;
    public double intelligence = 0.0;
    public double agility = 0.0;
    public double charm = 0.0;

    // Enemy stats
    public double intimidate = 0.0;  // Susceptibility to being intimidated. Only used for enemies. If between 0 and 1, acts as a multiplier instead.
    public double persuade = 0.0;    // Susceptibility to being persuaded. Only used for enemies. If between 0 and 1, acts as a multiplier instead.

    // Skill stats
    public double accuracy = 0.0;    // Accuracy to add or subtract from the inflicted unit's skills. If between 0 and 1, acts as a multiplier instead.

    public Status(string id)
    {
        switch (id)
        {
            case "Bleed":
                name = "Bleed";
                damage = 0.05;
                desc = "Loses HP at the start of each turn. Cured with most healing.";
                break;
            case "Drop Guard":
                name = "Drop Guard";
                maxStacks = 1;
                power = 0.8;
                persuade = 0.3;
                slashingDef = -0.2;
                piercingDef = -0.2;
                bluntDef = -0.2;
                break;
        }
    }
}
