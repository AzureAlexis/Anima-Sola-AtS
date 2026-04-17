using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public static class PartyManager
{
    public static List<Character> party;
    public static List<Character> characters;

    public static void Start()
    {
        CreateCharacterList();
    }


    static public void CreateCharacterList()
    {
        characters = new List<Character>();
    }
}
