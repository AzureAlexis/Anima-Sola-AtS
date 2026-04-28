using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Properties;

public static class TextManager
{
    static List<Scene> scenes = new List<Scene>();

    static TextMeshProUGUI textboxText;
    static TextMeshProUGUI textboxName;
    static RectTransform textboxRect;

    static TextMeshProUGUI choiceboxText;
    static TextMeshProUGUI choiceboxName;
    static RectTransform choiceboxRect;

    static RectTransform choiceCursor;

    static Scene currentScene;
    static Line currentLine;
    static Choice currentChoice;
    static int choiceIndex = 0;
    static bool choicePreserveTextbox = false;

    static int index = 0;
    static string state = "Inactive";
    static float debugTimer = 2;

    public static void Start()
    {
        BuildScript();
        GetReferences();
    }

    public static void Update()
    {
        UpdateAnimation();
        if (state != "Inactive") 
        {
            UpdateTextbox();
            UpdateLine();
            UpdateChoice();
        }
    }

    static void UpdateAnimation()
    {
        AnimateTextRect();
        AnimateTextbox();
        AnimateChoiceRect();
        AnimateChoicebox();
        AnimateChoiceCursor();
    }

    static void AnimateTextRect()
    {
        if ((state == "Line") || (state == "Choice" && choicePreserveTextbox) && textboxRect.sizeDelta.y < 300)
            textboxRect.sizeDelta = new Vector2(1628, Mathf.Min(textboxRect.sizeDelta.y + 3000 * Time.deltaTime, 300));
        else if (!((state == "Line") || (state == "Choice" && choicePreserveTextbox)) && textboxRect.sizeDelta.y > 0)
            textboxRect.sizeDelta = new Vector2(1628, Mathf.Max(textboxRect.sizeDelta.y - 3000 * Time.deltaTime, 0));
    }

    static void AnimateTextbox()
    {
        if (!((state == "Line") || (state == "Choice" && choicePreserveTextbox)) && textboxText.alpha != 0)
            textboxText.alpha = textboxName.alpha = 0;
        else if (((state == "Line") || (state == "Choice" && choicePreserveTextbox)) && textboxRect.sizeDelta.y == 300 && textboxText.alpha == 0)
            textboxText.alpha = textboxName.alpha = 1;
    }

    static void AnimateChoicebox()
    {
        if (state != "Choice" && choiceboxText.alpha != 0)
            choiceboxText.alpha = choiceboxName.alpha = 0;
        else if (state == "Choice" && choiceboxRect.sizeDelta.y == 300 && choiceboxText.alpha == 0)
            choiceboxText.alpha = choiceboxName.alpha = 1;
    }

    static void AnimateChoiceCursor()
    {
        if (state != "Choice")
            choiceCursor.gameObject.SetActive(false);
        else
        {
            choiceCursor.gameObject.SetActive(true);
            choiceCursor.anchoredPosition = new Vector2(70, -108 - 60 * choiceIndex);
        }
    }

    static void UpdateChoice()
    {
        if(state != "Choice")
            return;
        if (choiceboxText == null || choiceboxName == null)
            GetReferences();

        choiceboxText.text = $"    {currentChoice.choice1}\n    {currentChoice.choice2}\n    {currentChoice.choice3}";

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            choiceIndex = (choiceIndex + 1) % currentChoice.choices;
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            choiceIndex = (choiceIndex + currentChoice.choices - 1) % currentChoice.choices;
        }
    }

    static void AnimateChoiceRect()
    {
        if (state == "Choice" && choiceboxRect.sizeDelta.y < 300)
            choiceboxRect.sizeDelta = new Vector2(814, Mathf.Min(choiceboxRect.sizeDelta.y + 3000 * Time.deltaTime, 300 - (3 - currentChoice.choices) * 60));
        else if (state != "Choice" && choiceboxRect.sizeDelta.y > 0)
            choiceboxRect.sizeDelta = new Vector2(814, Mathf.Max(choiceboxRect.sizeDelta.y - 3000 * Time.deltaTime, 0));
    }

    public static void UpdateTextbox()
    {
        if (currentScene == null)
            return;
        if(textboxText == null || textboxName == null)
            GetReferences();

        textboxText.text = currentLine.text;
        textboxName.text = currentLine.speaker;
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
            if(state == "Choice")
            {
                Scene oldScene = currentScene;
                Debug.Log($"Choice {choiceIndex + 1} selected");
                switch (choiceIndex)
                {
                    case 0:
                        if (currentChoice.action1 != null)
                        {
                            currentChoice.action1?.Invoke();
                            return;
                        }
                        break;
                    case 1:
                        if (currentChoice.action2 != null)
                        {
                            currentChoice.action2?.Invoke();
                            return;
                        }
                        break;
                    case 2:
                        if (currentChoice.action3 != null)
                        {
                            currentChoice.action3?.Invoke();
                            return;
                        }
                        break;
                }
                if (currentScene != oldScene)
                    return;
            }
            if (index == currentScene.lines.Count - 1)
            {
                state = "Inactive";
                EndScene();
                return;
            }
            
            index++;

            if(currentScene.lines[index] is Choice)
            {
                state = "Choice";
                currentChoice = currentScene.lines[index] as Choice;
                choiceIndex = 0;
                choicePreserveTextbox = currentChoice.preserveTextbox;
            }
            else
            {
                state = "Line";
                currentLine = currentScene.lines[index];
            }
        }

    }
    static void GetReferences()
    {
        textboxText = GameObject.Find("TextboxText").GetComponent<TextMeshProUGUI>();
        textboxName = GameObject.Find("TextboxName").GetComponent<TextMeshProUGUI>();
        textboxRect = GameObject.Find("Textbox").GetComponent<RectTransform>();

        choiceboxText = GameObject.Find("ChoiceboxText").GetComponent<TextMeshProUGUI>();
        choiceboxRect = GameObject.Find("Choicebox").GetComponent<RectTransform>();
        choiceboxName = GameObject.Find("ChoiceboxName").GetComponent<TextMeshProUGUI>();

        choiceCursor = GameObject.Find("ChoiceCursor").GetComponent<RectTransform>();
    }
    public static void StartScene(string name)
    {
        Interpreter.Enqueue(typeof(TextManager));
        currentScene = scenes.Find(scene => scene.name == name);
        index = 0;
        if (currentScene.lines[index] is Choice)
        {
            state = "Choice";
            currentChoice = currentScene.lines[index] as Choice;
            choiceIndex = 0;
            choicePreserveTextbox = currentChoice.preserveTextbox;
        }
        else
        {
            state = "Line";
            currentLine = currentScene.lines[index];
        }
    }

    public static void EndScene()
    {
        Interpreter.Dequeue(typeof(TextManager));
        currentScene = null;
        index = 0;
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
                    },
                    new Choice()
                    {
                        choices = 3,
                        choice1 = "Test choice 1",
                        choice2 = "Test choice 2",
                        choice3 = "Test choice 3",
                        action1 = () => StartScene("AuroraWakeup"),
                        action2 = () => Debug.Log("Choice 2 selected"),
                        action3 = () => Debug.Log("Choice 3 selected")
                    }
                }
            },
            new Scene { name = "AuroraWakeup",
                lines = new List<Line> {
                    new Choice() {
                        choices = 2,
                        choice1 = "Get up",
                    }
                }
            },
            new Scene { name = "Day_1_1_MeetVivian",
                lines  = new List<Line>
                {
                    new Line()
                    {
                        speaker = "System",
                        text = "You see a person looking across the lake. She looks rather nervous."
                    },
                    new Choice()
                    {
                        choices = 3,
                        choice1 = "'Who's there?'",
                        choice2 = "'Are you alright?'",
                        choice3 = "Attack",
                        action3 = () => BattleManager.StartBattle()
                    }
                }

            }
        };
    }
}
