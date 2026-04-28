using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUi : MonoBehaviour
{
    public static BattleUi Instance;
    bool active = false;

    public int selectedTopCommand;

    List<Image> topMenuOptions;
    RectTransform skillMenuRect;
    List<Image> skillMenuOptions;
    List<TextMeshProUGUI> skillMenuText;

    Color brightRed = new Color(1f, 0.4f, 0.4f);
    Color darkRed = new Color(0.6f, 0.2f, 0.2f);
    void Start()
    {
        Instance = this;
        Util.SetChildrenActive(transform, false);
        StartBattle();
    }
    void Update()
    {
        if (active)
        {
            UpdateAnimation();
        }
    }

    public void StartBattle()
    {
        Util.SetChildrenActive(transform, true);
        active = true;
    }

    public void EndBattle()
    {
        Util.SetChildrenActive(transform, false);
        active = false;
    }

    void UpdateAnimation()
    {
        AnimateTopMenu();
        AnimateSkillMenu();
    }

    void AnimateSkillMenu()
    {
        if (BattleManager.State() == "SkillSelect")
        {
            skillMenuRect.anchoredPosition = new Vector2(535, -172 - (66 * selectedTopCommand));
            for (int i = 0; i < 4; i++)
            {
                skillMenuOptions[i].gameObject.SetActive(true);

                if (i != BattleManager.Index() && skillMenuOptions[i].color != brightRed)
                    skillMenuOptions[i].color = brightRed;
                else if (i == BattleManager.Index() && skillMenuOptions[i].color != Color.white)
                    skillMenuOptions[i].color = Color.white;

                try { 
                    skillMenuText[i * 2].text = BattleManager.subCommands[i].name;
                    skillMenuText[i * 2 + 1].text = "AP " + BattleManager.subCommands[i].cost.ToString();
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    skillMenuText[i * 2].text = "-";
                    skillMenuText[i * 2 + 1].text = "";
                    skillMenuOptions[i].color = darkRed;
                }


            }
        }
        else
        {
            for (int i = 0; i < skillMenuOptions.Count; i++)
                skillMenuOptions[i].gameObject.SetActive(false);
        }
    }


    void AnimateTopMenu()
    {
        Color inactiveColor = brightRed;
        if(BattleManager.State() == "TopMenu")
            selectedTopCommand = BattleManager.Index();
        else
            inactiveColor = darkRed;


        if (topMenuOptions == null || topMenuOptions.Contains(null))
            GetRefrences();
        for (int i = 0; i < topMenuOptions.Count; i++)
        {
            if (i != selectedTopCommand && topMenuOptions[i].color != inactiveColor)
                topMenuOptions[i].color = inactiveColor;
            else if(i == selectedTopCommand && topMenuOptions[i].color != Color.white)
                topMenuOptions[i].color = Color.white;
        }
    }

    void GetRefrences()
    {
        topMenuOptions = new List<Image>(GameObject.Find("TopMenu").GetComponentsInChildren<Image>());
        skillMenuRect = GameObject.Find("SkillMenu").GetComponent<RectTransform>();
        skillMenuOptions = new List<Image>(GameObject.Find("SkillMenu").GetComponentsInChildren<Image>());
        skillMenuText = new List<TextMeshProUGUI>(GameObject.Find("SkillMenu").GetComponentsInChildren<TextMeshProUGUI>());
    }
}
