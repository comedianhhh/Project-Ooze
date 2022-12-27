using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class EnvironmentPlacer : MonoBehaviour
{
    [SerializeField] Transform propParent;
    [SerializeField] List<GameObject> prop1x1Prefabs = new List<GameObject>();
    [SerializeField] List<GameObject> prop3x1Prefabs = new List<GameObject>();
    [SerializeField] List<GameObject> WallpropPrefabs = new List<GameObject>();



    [SerializeField] List<GameObject> placedProps = new List<GameObject>();
    [SerializeField] List<Vector2Int> placedPositions = new List<Vector2Int>();

    public void CreatProps(List<Vector2Int> wallPositions, List<Vector2Int> floorPositions, Tilemap walltilemap,Tilemap floortilmemap)
    {

        placedPositions.Clear();

        for (int i = 0; i < placedProps.Count; i++)
        {
            if (placedProps[i])
                DestroyImmediate(placedProps[i].gameObject);
        }
        placedProps.Clear();

        Create3x1Props(wallPositions, walltilemap);
        Create1x1Props(wallPositions, walltilemap);
        CreateWallProps(wallPositions,floorPositions, floortilmemap);
    }

    void Create1x1Props(List<Vector2Int> wallPositions, Tilemap tilemap)
    {
        wallPositions = wallPositions.Distinct().Except(placedPositions).ToList();
        foreach (var pos in wallPositions)
        {
            var up = pos + Vector2Int.up;
            var down = pos + Vector2Int.down;
            var left = pos + Vector2Int.left;
            var right = pos + Vector2Int.right;

            var adjacentWallCount = 0;
            if (wallPositions.Contains(up)) adjacentWallCount++;
            if (wallPositions.Contains(down)) adjacentWallCount++;
            if (wallPositions.Contains(left)) adjacentWallCount++;
            if (wallPositions.Contains(right)) adjacentWallCount++;

            if ((adjacentWallCount == 0 || adjacentWallCount == 1) && Random.Range(0, 3) < 2)
            {
                var prefab = prop1x1Prefabs[Random.Range(0, prop1x1Prefabs.Count)];
                var worldPos = tilemap.CellToWorld((Vector3Int)pos) + new Vector3(0.5f, 0.5f, 0);
                var prop = Instantiate(prefab, worldPos, Quaternion.identity, propParent);
                placedProps.Add(prop);
                tilemap.SetTile((Vector3Int)pos, null);
            }
        }
    }
    void Create3x1Props(List<Vector2Int> wallPositions, Tilemap tilemap)
    {
        wallPositions = wallPositions.Distinct().ToList();
        foreach (var pos in wallPositions)
        {
            var up = pos + Vector2Int.up;
            var down = pos + Vector2Int.down;
            var left = pos + Vector2Int.left;
            var rightup = pos + Vector2Int.right+ Vector2Int.up;
            var rightdown = pos + Vector2Int.right + Vector2Int.down;
            var rightup2 = pos + Vector2Int.right*2 + Vector2Int.up;
            var rightdown2 = pos + Vector2Int.right *2+ Vector2Int.down;
            var right = pos + Vector2Int.right * 3;

            var adjacentWallCount = 0;
            if (wallPositions.Contains(up)) adjacentWallCount++;
            if (wallPositions.Contains(down)) adjacentWallCount++;
            if (wallPositions.Contains(left)) adjacentWallCount++;
            if (wallPositions.Contains(rightup)) adjacentWallCount++;
            if (wallPositions.Contains(rightdown)) adjacentWallCount++;
            if (wallPositions.Contains(rightup2)) adjacentWallCount++;
            if (wallPositions.Contains(rightdown2)) adjacentWallCount++;
            if (wallPositions.Contains(right)) adjacentWallCount++;

            // todo: right 2 walls

            var WallRight = pos + Vector2Int.right;
            var WallRight2 = pos + Vector2Int.right * 2;
            var selfWallCount = 0;
            if (wallPositions.Contains(WallRight)) selfWallCount++;
            if (wallPositions.Contains(WallRight2)) selfWallCount++;

            // todo: if (contains) ++

            if ((adjacentWallCount == 0&& selfWallCount==2)) // todo: && selfWallCount == 2
            {
                var prefab = prop3x1Prefabs[Random.Range(0, prop1x1Prefabs.Count)];
                var worldPos = tilemap.CellToWorld((Vector3Int)pos);
                var prop = Instantiate(prefab, worldPos, Quaternion.identity, propParent);
                placedProps.Add(prop);
                tilemap.SetTile((Vector3Int)pos, null);
                placedPositions.Add(pos);
            }
        }
    }
    void CreateWallProps(List<Vector2Int> WallPositions, List<Vector2Int> floorPositions, Tilemap tilemap)
    {

        foreach (var pos in floorPositions)
        {
            var up = pos + Vector2Int.up;
            var down = pos + Vector2Int.down;
            var left = pos + Vector2Int.left;
            var right = pos + Vector2Int.right;

            var adjacentWallCount = 0;
            if (WallPositions.Contains(up)) adjacentWallCount++;
            if (WallPositions.Contains(down)) adjacentWallCount++;
            if (WallPositions.Contains(left)) adjacentWallCount++;
            if (WallPositions.Contains(right)) adjacentWallCount++;

            if ((adjacentWallCount == 2 || adjacentWallCount == 1) && Random.Range(0, 10) < 2)
            {
                var prefab = WallpropPrefabs[Random.Range(0, WallpropPrefabs.Count)];
                var worldPos = tilemap.CellToWorld((Vector3Int)pos) + new Vector3(0.5f, 0.5f, 0);
                var prop = Instantiate(prefab, worldPos, Quaternion.identity, propParent);
                placedProps.Add(prop);
            }
        }
    }

    public void Clear()
    {
        foreach (var prop in placedProps)
        {
        if (prop)
                DestroyImmediate(prop);
        }
        placedProps.Clear();
    }    
}
