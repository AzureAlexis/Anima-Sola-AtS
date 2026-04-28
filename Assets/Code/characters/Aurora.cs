using UnityEngine;

public class Aurora : Character
{
    public Aurora()
    {
        name = "Aurora";
        power = 8;
        stamina = 9;
        agility = 8;
        intelligence = 9;
        charisma = 10;

        weapon = EquipmentManager.GetWeapon("Knife");
        armor = EquipmentManager.GetArmor("Aurora's Jacket");
        charms.Add(EquipmentManager.GetCharm("Woven Bracelet"));
    }
    new public void Start()
    {
        name = "Aurora";
        shortDesc = "A novice journalist who can talk her way out of most situations.";
        power = 8;
        stamina = 9;
        agility = 8;
        intelligence = 9;
        charisma = 10;

        weapon = EquipmentManager.GetWeapon("Knife");
        armor = EquipmentManager.GetArmor("Aurora's Jacket");
        charms.Add(EquipmentManager.GetCharm("Woven Bracelet"));
    }
}
