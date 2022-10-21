using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DungeonCrawler : MonoBehaviour
{

    public Vector2Int Postion { get; set; }
    public DungeonCrawler(Vector2Int startPos)
    {
        Postion = startPos;
    }

    public Vector2Int Move(Dictionary<Direction, Vector2Int> directionMovementMap)
    {
        Direction toMove = (Direction) Random.Range(0, directionMovementMap.Count);
        Postion += directionMovementMap[toMove];
        return Postion;
    }
}
