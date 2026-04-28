using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BattleCursor : MonoBehaviour
{
    public bool active = false;
    public static BattleCursor Instance;
    public static RectTransform rect;
    void Start()
    {
        if(Instance != null)
            Destroy(Instance.gameObject);
        Instance = this;
        rect = GetComponent<RectTransform>();
    }
    void Update()
    {
        if(active)
        {
            Move();
            Select();
        }
    }
    private void Select()
    {
        if (!Input.GetKeyDown(KeyCode.Z))
            return;

        Vector2 selectedTile = Util.ConvertToTile(transform.position);
        foreach (Character character in PartyManager.party)
        {
            if (Util.ConvertToTile(character.gameObject.transform.position) == selectedTile)
            {
                BattleManager.Select(character);
                return;
            }
        }

        foreach (Enemy enemy in EnemyManager.activeEnemies)
        {
            if (Util.ConvertToTile(enemy.gameObject.transform.position) == selectedTile)
            {
                BattleManager.Select(enemy);
                return;
            }
        }

        BattleManager.Select(selectedTile);
    }
    private void Move()
    {
        Vector2 move = Vector2.zero;
        if (Input.GetKey(KeyCode.UpArrow))
            move += Vector2.up;
        if (Input.GetKey(KeyCode.DownArrow))
            move += Vector2.down;
        if (Input.GetKey(KeyCode.LeftArrow))
            move += Vector2.left;
        if (Input.GetKey(KeyCode.RightArrow))
            move += Vector2.right;
        rect.anchoredPosition += move * 1000 * Time.deltaTime;
        if(rect.anchoredPosition.x > 500)
        {
            float dif = rect.anchoredPosition.x - 500;
            CameraController.Instance.Move(new(dif, 0));
            rect.anchoredPosition = new(500, rect.anchoredPosition.y);
        }
        if(rect.anchoredPosition.y > 500)
        {
            float dif = rect.anchoredPosition.y - 500;
            CameraController.Instance.Move(new(0, dif));
            rect.anchoredPosition = new(rect.anchoredPosition.x, 500);
        }
        if (rect.anchoredPosition.x < -500)
        {
            float dif = rect.anchoredPosition.x + 500;
            CameraController.Instance.Move(new(dif, 0));
            rect.anchoredPosition = new(-500, rect.anchoredPosition.y);
        }
        if (rect.anchoredPosition.y < -500)
        {
            float dif = rect.anchoredPosition.y + 500;
            CameraController.Instance.Move(new(0, dif));
            rect.anchoredPosition = new(rect.anchoredPosition.x, -500);
        }
    }

    public void Activate()
    {
        active = true;
    }
    public void Deactivate()
    {
        active = false;
    }
}