using System;
using UnityEngine;

public static class Util
{
    public static void SetChildrenActive(Transform parent, bool active, bool includeRoot = false)
    {
        foreach (Transform child in parent)
        {
            SetChildrenActive(child, active);
            child.gameObject.SetActive(active);
        }

        if (includeRoot)
            parent.gameObject.SetActive(active);
    }
    public static Vector2 ConvertToTile(Vector2 position)
    {
        return new Vector2((float)(Math.Floor(position.x) + 0.5f), (float)(Math.Floor(position.y) + 0.5f));
    }
    public static int DistanceToTile(Vector2 position, Vector2 tile)
    {
        return (int)(Math.Abs(position.x - tile.x) + Math.Abs(position.y - tile.y));
    }
}
