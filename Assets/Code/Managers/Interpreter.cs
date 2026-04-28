using System.Collections.Generic;
using UnityEngine;

public class Interpreter : MonoBehaviour
{
    static bool busy = false;
    static List<System.Type> queue = new List<System.Type>();

    void Start()
    {
        SkillManager.Start();
        EquipmentManager.Start();
        PartyManager.Start();
        TextManager.Start();
        BattleManager.Start();
    }

    void Update()
    {
        BattleManager.Update();
        TextManager.Update();
    }
    public static void Enqueue(System.Type Obj)
    {
        queue.Add(Obj);
        busy = true;
    }
    public static void Dequeue(System.Type Obj)
    {
        queue.Remove(Obj);
        if(queue.Count <= 0 )
            busy = false;
    }
    public static bool Busy() {return busy;}
    public static bool Ready() {return !busy;}
    public static bool Ready(System.Type Obj)
    {
        return queue[0] == Obj;
    }
}
