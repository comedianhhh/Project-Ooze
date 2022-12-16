using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class EnvironmentPlacer : MonoBehaviour
{
    [SerializeField] Transform propParent;
    [SerializeField] List<GameObject> prop1x1Prefabs = new List<GameObject>();

    [SerializeField] List<GameObject> placedProps = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Create1x1Props(List<Vector2Int> wallPositions, Tilemap tilemap)
    {
        wallPositions = wallPositions.Distinct().ToList();

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
