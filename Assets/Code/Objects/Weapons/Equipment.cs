using UnityEngine;

public class Equipment
{
    public string name;
    public string desc;
    public double slashingDef = 0.0;    // Percentage reduction in damage from slashing attacks
    public double piercingDef = 0.0;    // Percentage reduction in damage from piercing attacks
    public double bluntDef = 0.0;       // Percentage reduction in damage from blunt attacks
    public double magicDef = 0.0;       // Percentage reduction in damage from magic attacks. Does not affect psychic attacks.
    public int power = 0;               // Additional power added to the character's attacks when this equipment is equipped. This is a flat increase, not a percentage.
    public int agility = 0;             // Additional agility added to the character when this equipment is equipped
    public int charisma = 0;
    public int stamina = 0;
    public int intelligence = 0;
}
