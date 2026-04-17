using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int hp;
    public int mhp;
    public int power;
    public int stamina;
    public int intelligence;
    public int agility;
    public int charisma;
    public Weapon weapon;
    public Equipment armor;
    public List<Equipment> charms = new List<Equipment>();
    public List<Status> statusEffects = new List<Status>();
    public List<(Weapon, int)> weaponProficiencies = new List<(Weapon, int)>();

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
    public void TakeDamage(int damage, string damageType = "Untyped")
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
    public void Equip(object item)
    {
        switch (item)
        {
            case Weapon w:
                if (weapon != null) EquipmentManager.AddHeldWeapon(weapon.name);
                weapon = EquipmentManager.RemoveHeldWeapon(w.name);
                break;
            case Armor a:
                if (armor != null) EquipmentManager.AddHeldArmor(armor.name);
                armor = EquipmentManager.RemoveHeldArmor(a.name);
                break;
            case Equipment e:
                charms.Add(EquipmentManager.RemoveHeldCharm(e.name));
                break;
            default:
                Debug.LogWarning($"Cannot equip object of unsupported type: {item?.GetType().Name}");
                break;
        }
    }

    // Status effect functions
    public void UpdateStatusEffects()
    {
        foreach (Status s in statusEffects)
        {
            if (s.damage != 0)
            {
                if (s.damage < 1) TakeDamage((int)(s.damage * hp));
                else TakeDamage((int)s.damage);
            }
            if (s.duration != -1)
            {
                s.duration--;
                if (s.duration == 0 && !s.persist) statusEffects.Remove(s);
            }
        }
    }
    public void CureStatus(string status)
    {
        foreach (Status s in statusEffects)
        {
            if (s.name == status)
            {
                s.stacks = 0;
            }
        }
    }
    public void DecreaseStatus(string status, int amount = 1)
    {
        foreach (Status s in statusEffects)
        {
            if (s.name == status)
            {
                s.stacks -= amount;
                if (s.stacks <= 0) statusEffects.Remove(s);
            }
        }
    }
    public void InflictStatus(string status, int count = 1)
    {
        foreach (Status s in statusEffects)
        {
            if (s.name == status)
            {
                s.stacks += count;
                return;
            }
        }
        statusEffects.Add(new Status(status));
    }
    
    // Stat getter Functions
    public int TotalPower()
    {
        int totalPower = power;

        if (weapon != null) totalPower += weapon.power;
        if (armor != null) totalPower += armor.power;
        if (charms != null)
        {
            foreach (Equipment e in charms)
                totalPower += e.power;
        }
        foreach (Status s in statusEffects)
        {
            if(s.power != 0)
            {
                if(s.power > 1) totalPower += (int)(s.power);
                else totalPower *= (int)(s.power * totalPower);
            }
        }

        return totalPower;
    }
    public int TotalStamina()
    {
        int total = stamina;

        if (weapon != null) total += weapon.stamina;
        if (armor != null) total += armor.stamina;
        if (charms != null)
        {
            foreach (Equipment e in charms)
                total += e.stamina;
        }
        foreach (Status s in statusEffects)
        {
            if (s.stamina != 0)
            {
                if (s.stamina > 1) total += (int)(s.stamina);
                else total *= (int)(s.stamina * total);
            }
        }
        return total;
    }
    public int TotalAgility()
    {
        int total = agility;

        if (weapon != null) total += weapon.agility;
        if (armor != null) total += armor.agility;
        if (charms != null)
        {
            foreach (Equipment e in charms)
                total += e.agility;
        }
        foreach (Status s in statusEffects)
        {
            if (s.agility != 0)
            {
                if (s.agility > 1) total += (int)(s.agility);
                else total *= (int)(s.agility * total);
            }
        }
        return total;
    }
    public int TotalIntelligence()
    {
        int total = intelligence;

        if (weapon != null) total += weapon.intelligence;
        if (armor != null) total += armor.intelligence;
        if (charms != null)
        {
            foreach (Equipment e in charms)
                total += e.intelligence;
        }
        foreach (Status s in statusEffects)
        {
            if (s.intelligence != 0)
            {
                if (s.intelligence > 1) total += (int)(s.intelligence);
                else total *= (int)(s.intelligence * total);
            }
        }
        return total;
    }
    public int TotalCharisma()
    {
        int total = charisma;

        if (weapon != null) total += weapon.charisma;
        if (armor != null) total += armor.charisma;
        if (charms != null)
        {
            foreach (Equipment e in charms)
                total += e.charisma;
        }
        foreach (Status s in statusEffects)
        {
            if (s.charisma != 0)
            {
                if (s.charisma > 1) total += (int)(s.charisma);
                else total *= (int)(s.charisma * total);
            }
        }
        return total;
    }
    public int TotalStat(string stat)
    {
        switch (stat)
        {
            case "power":
                return TotalPower();
            case "stamina":
                return TotalStamina();
            case "agility":
                return TotalAgility();
            case "intelligence":
                return TotalIntelligence();
            case "charisma":
                return TotalCharisma();
        }
        return 0;
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
    public double TotalDef(string damageType)
    {
        switch (damageType)
        {
            case "Blunt":
                return TotalBluntDef();
            case "Slashing":
                return TotalSlashingDef();
            case "Piercing":
                return TotalPiercingDef();
            case "Magic":
                return TotalMagicDef();
        }
        return 0;
    }
    public int StatusStacks(string status)
    {
        foreach(Status s in statusEffects)
            if(s.name == status) return s.stacks;
        return 0;
    }
    
}
