using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.TextCore.Text;

public static class PartyManager
{
    public static List<Character> party = new List<Character>();
    public static List<Character> characters = new List<Character>();

    public static void Start()
    {
        CreateCharacterList();
    }


    static void CreateCharacterList()
    {
        characters = new List<Character>();
        party.Add(new Aurora());
        party[0].Start();
    }

    public static Character Get(string name)
    {
        foreach (Character character in party) 
        {
            Debug.Log(character.name);
            if(character.name == name)
                return character;
        }

        return null;
    }
}
