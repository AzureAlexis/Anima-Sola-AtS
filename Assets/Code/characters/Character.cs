using System.Collections.Generic;
using UnityEngine;

public class Character : Unit
{
    public string name;
    public string shortDesc;
    public string longDesc;

    new public int power;
    new public int stamina = 10;     // Multiplied by 5 to determine the character's max HP
    new public int agility = 8;     // How many action points the character generates per turn
    new public int charm = 5;       // Affects the success rate of social skills
    new public int intelligence = 5; // Affects the success rate of technical skills

    public List<Skill> skills = new List<Skill>();
    public List<Skill> moves = new List<Skill>();

    public List<Skill> WeaponSkills()
    {
        return weapon.skills;
    }

    public List<Skill> Skills()
    {
        return skills;
    }

    public List<Skill> Moves()
    {
        return moves;
    }
    public List<Skill> Items()
    {
        // Implement items later
        return new List<Skill>();
    }
}
