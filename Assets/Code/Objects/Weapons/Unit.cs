using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public List<Status> statusEffects;
    public int hp;
    public int mhp;
    public int power;
    public int stamina;
    public int intelligence;
    public int agility;
    public int charisma;
    public Weapon weapon;
    public Equipment armor;
    public List<Equipment> charms;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool InControlZone()
    {
        return false;
        // Implement check if this unit is in any other unit's zone of control.
    }
    public void TakeDamage(int damage, string damageType)
    {
        switch(damageType)
        {
            case "Blunt":
                damage = (int)(damage * (1 - TotalBluntDef()));
                break;
            case "Slashing":
                damage = (int)(damage * (1 - TotalSlashingDef()));
                break;
            case "Piercing":
                damage = (int)(damage * (1 - TotalPiercingDef()));
                break;
            case "Magic":
                damage = (int)(damage * (1 - TotalMagicDef()));
                break;
        }
        hp -= damage;
        if(hp < 0) hp = 0;
    }
    public int TotalPower()
    {
        if(weapon != null) return power + weapon.power;
        return power;
    }

    public double TotalBluntDef()
    {
        double def = armor.bluntDef;
        foreach(Equipment charm in charms)
            def += charm.bluntDef;
        return math.max(def, 0.8);
    }
    public double TotalSlashingDef()
    {
        double def = armor.slashingDef;
        foreach(Equipment charm in charms)
            def += charm.slashingDef;
        return math.max(def, 0.8);
    }
    public double TotalPiercingDef()
    {
        double def = armor.piercingDef;
        foreach(Equipment charm in charms)
            def += charm.piercingDef;
        return math.max(def, 0.8);
    }
    public double TotalMagicDef()
    {
        double def = armor.magicDef;
        foreach(Equipment charm in charms)
            def += charm.magicDef;
        return math.max(def, 0.8);
    }

    public int StatusStacks(string status)
    {
        foreach(Status s in statusEffects)
            if(s.name == status) return s.stacks;
        return 0;
    }
    public void CureStatus(string status)
    {
        foreach(Status s in statusEffects)
        {
            if(s.name == status)
            {
                s.stacks = 0;
            }
        }
    }

    public void DecreaseStatus(string status, int amount = 1)
    {
        foreach(Status s in statusEffects)
        {
            if(s.name == status)
            {
                s.stacks -= amount;
                if(s.stacks <= 0) statusEffects.Remove(s);
            }
        }
    }

    public void InflictStatus(string status, int count = 1)
    {
        foreach(Status s in statusEffects)
        {
            if(s.name == status)
            {
                s.stacks += count;
                return;
            }
        }
        statusEffects.Add(new Status(status));
    }
}
