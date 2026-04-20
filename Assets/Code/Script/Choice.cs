using UnityEngine;

public class Choice : Line
{
    public string choice1;  // Text to display for first choice
    public string choice2;
    public string choice3;
    public System.Action action1;     // Action to execute if first choice is selected
    public System.Action action2;
    public System.Action action3;
    public bool preserveTextbox = true;   // Whether the textbox should remain open when choicebox is opened
    public int choices;

    public Choice()
    {

    }
}
