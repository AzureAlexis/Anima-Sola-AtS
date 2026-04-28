using UnityEngine;

public class Skill
{
    // Basic info
    public string name;                 // The name of the skill to show in the UI
    public string desc;                 // A description of the skill to show in the UI
    public string debugDesc;            // A more literal description for skills with vague descriptions.
    public int type = 0;                // What type of skill this is. 0 for attacks, 1 for heals, 2 for moves.
    public int target;                  // The type of units this skill can target. 0 for enemies, 1 for allies, 2 for self.
    public int cost = 0;                // How many action points this skill costs to use.

    // Damage info
    public string stat = "Power";
    public double power = 1.0;     // How much to multiply the user's power by to get the skill's damage.
    public double accuracy = 0.9;       // The chance for this skill to hit. Ignored if target is 1 or 2.
    public string damageType = "Slashing";

    // Hits
    public int maxHits = 1;          // The maximum number of hits this skill can do. If same as minHits, the skill will always do that many hits, otherwise it will do a random number of hits between minHits and maxHits.
    public int minHits = 1;          // The minimum number of hits it can do.

    // Status effects
    public string inflictStatus;         // If not null, the status effect this skill inflicts on hit.
    public double inflictAcc = 1.0;         // The chance for this skill to inflict its status effect.
    public string inflictAccStat;     // If not null, multiply the inflictAcc by this stat.
    public string cureStatus;           // If not null, the status effect this skill cures on hit.
    public double cureAcc = 1.0;           // The chance for this skill to cure its status effect.
    public string cureAccStat;       // If not null, multiply the cureAcc by this stat.

    // Misc info
    public int recoil;              // How much damage the user takes when using this skill. Only for otherworldy skills.

    public System.Func<bool> specialCondition;  // If condition is true, triggers the special effect of the skill.
    public System.Action specialEffect;

    // Move info
    public int distance = 0;            // If this skill is a move, how many tiles it can move the user. If -1, infinite tiles.

    public string special;              // What script to run, for skills like Run or Equip.

    public Skill(){}
}
