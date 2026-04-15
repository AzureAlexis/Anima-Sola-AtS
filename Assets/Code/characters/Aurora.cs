using UnityEngine;

public class Aurora : Character
{
    public string name = "Aurora";
    public string shortDesc = "A novice journalist who can talk her way out of most situations.";
    public Weapon weapon = EquipmentManager.GetWeapon("Knife");
    public Armor armor = EquipmentManager.GetArmor("Aurora's Jacket");
    public Charm charm = EquipmentManager.GetCharm("Woven Bracelet");

    Aurora()
    {
        power = 8;
        stamina = 9;
        agility = 8;
        intelligence = 9;
        charisma = 10;
    }
}
