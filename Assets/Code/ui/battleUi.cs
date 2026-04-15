using UnityEngine;
using System.Collections.Generic;

public class battleUi : MonoBehaviour
{
    public int layer = 0;   // Which menu layer the player is on. 0 is top menu, 1 is skill select, etc.
    List<string> topCommands = new List<string> {"Fight", "Tech", "Talk", "Item", "Move", "Item", "Tactic"};
    List<Skill> subCommands = new List<Skill> {};
    Character selectedCharacter;  // The character whose commands are being displayed.
    int index = 0;  // Index of the currently selected command.

    void Update()
    {
        UpdateIndex();
        UpdateLayer();
    }

    void UpdateIndex()
    {
        int maxIndex = 0;
        switch (layer)
        {
            case 0:
                maxIndex = PartyManager.party.Count - 1;
                break;
            case 1:
                maxIndex = topCommands.Count - 1;
                break;
            case 2:
                maxIndex = subCommands.Count - 1;
                break;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
            index++;
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            index--;

        if (index > maxIndex)
            index = 0;
        else if (index < 0)
            index = maxIndex;
    }

    void UpdateLayer()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            switch (layer)
            {
                case 0:
                    BuildSkillList();
                    break;
                case 1:
                    // Execute the selected skill.
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            if (layer > 0)
                layer--;
        }
    }

    void BuildSkillList()
    {
        subCommands.Clear();

        switch (index)
        {
            case 0:
                subCommands.AddRange(selectedCharacter.WeaponSkills());
                break;
            case 1:
                subCommands.AddRange(selectedCharacter.Skills());
                break;
            case 2:
                subCommands.AddRange(SkillManager.SocialSkills());
                break;
            case 3:
                subCommands.AddRange(selectedCharacter.Moves());
                break;
            case 4:
                subCommands.AddRange(selectedCharacter.Items());
                break;
            case 5:
                subCommands.AddRange(SkillManager.Tactics());
                break;
        }
    }

    
}
