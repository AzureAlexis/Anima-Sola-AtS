using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class TextManager
{
    static List<Scene> scenes = new List<Scene>();
    static TextMeshProUGUI textbox;
    static TextMeshProUGUI namebox;
    static Scene currentScene;
    static Line currentLine;
    static int index = 0;

    public static void Start()
    {
        BuildScript();
        GetReferences();
        StartScene("Intro");
    }

    public static void Update()
    {
        if (currentScene != null) 
        {
            Debug.Log(index);
            UpdateTextbox();
            UpdateLine();
        }
    }

    public static void UpdateTextbox()
    {
        if (currentScene == null)
            return;
        if(textbox == null || namebox == null)
            GetReferences();

        currentLine = currentScene.lines[index];

        textbox.text = currentLine.text;
        namebox.text = currentLine.speaker;
    }
    public static void UpdateLine()
    {
        if (currentScene == null)
        {
            Debug.LogWarning("Tried to continue scene, but no scene is currently active.");
            return;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (index == currentScene.lines.Count - 1)
                EndScene();
            else
                index++;
        }

    }
    static void GetReferences()
    {
        textbox = GameObject.Find("Textbox").GetComponent<TextMeshProUGUI>();
        namebox = GameObject.Find("Namebox").GetComponent<TextMeshProUGUI>();
    }

    static void BuildScript()
    {
        scenes = new List<Scene>
        {
            new Scene { name = "Intro",
                lines = new List<Line> {
                    new Line() {
                        text = "Did you really think I wouldn't notice you there, Dameon?",
                        speaker = "???"
                    },
                    new Line() {
                        text = "...It's not like I was trying to sneak up on you or anything.",
                        speaker = "???"
                    },
                    new Line() {
                        text = "What brings you here today? The same reason as myself?",
                        speaker = "???"
                    },
                    new Line() {
                        text = "I dunno. Mostly 'cause I knew you'd be here",
                        speaker = "Dameon"
                    },
                    new Line() {
                        text = "Aw, how sweet of you.",
                        speaker = "???"
                    },
                    new Line() {
                        text = "...I know you're mocking me, Kozera. I'm not that stupid.",
                        speaker = "Dameon"
                    },
                    new Line() {
                        text = "I'm sure you've noticed it too. The storm, the one moving in from the east.",
                        speaker = "Kozera"
                    },
                    new Line() {
                        text = "A storm that threatens the bounds of Logic. Perhaps smaller than the last incident, but a threat nonetheless.",
                        speaker = "Kozera"
                    },
                    new Line() {
                        text = "Quite perceptive of you.",
                        speaker = "Dameon"
                    },
                    new Line() {
                        text = "Why, it's my job to be perceptive.",
                        speaker = "Kozera"
                    },
                    new Line() {
                        text = "Don't you call yourself an 'impartial observer' or something?",
                        speaker = "Dameon"
                    },
                    new Line() {
                        text = "Can't I be both?",
                        speaker = "Kozera"
                    },
                    new Line() {
                        text = "Touche.",
                        speaker = "Dameon"
                    },
                    new Line() {
                        text = "...am I overreacting, Dameon?",
                        speaker = "Kozera"
                    },
                    new Line() {
                        text = "I don't... I don't wanna talk about that right now. Can we just enjoy the rest of today, and talk about that tomorrow?",
                        speaker = "Dameon"
                    },
                    new Line() {
                        text = "...sure. That's alright.",
                        speaker = "Kozera"
                    }
                }
            }
        };
    }

    public static void StartScene(string name)
    {
        Interpreter.Enqueue(typeof(TextManager));
        currentScene = scenes.Find(scene => scene.name == name);
        currentLine = currentScene.lines[0];
        index = 0;
    }

    public static void EndScene()
    {
        Interpreter.Dequeue(typeof(TextManager));
        currentScene = null;
        index = 0;
    }
}
