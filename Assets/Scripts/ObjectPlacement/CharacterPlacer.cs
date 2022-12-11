using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

public class CharacterPlacer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform player;
    [SerializeField] List<CharacterPlacerPattern> enemyPlacerPatterns = new List<CharacterPlacerPattern>();
    [SerializeField] CharacterPlacerPattern bossPattern = new CharacterPlacerPattern();

    [Header("Settings")]
    [SerializeField] float detectionRadius = 5;

    [Header("Data")]
    [SerializeField] List<Enemy> placedEnemies = new List<Enemy>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void PlaceEveryRoom(Vector3[] roomPositions, Vector3[] floorWorldPositions)
    {
        if (roomPositions.Length == 0 || floorWorldPositions.Length == 0) return;

        player.transform.position = roomPositions[0];

        for (int i = 0; i < placedEnemies.Count; i++)
            DestroyImmediate(placedEnemies[i].gameObject);
        placedEnemies.Clear();

        for (int i = 0; i < roomPositions.Length; i++)
        {
            if (i == roomPositions.Length - 1 && bossPattern.Enemies.Count > 0)
                Place(roomPositions[i], floorWorldPositions, bossPattern);
            else
                Place(roomPositions[i], floorWorldPositions);
        }
    }

    public void Place(Vector3 spawnPosition, Vector3[] floorWorldPositions, CharacterPlacerPattern presetPattern = null)
    {
        List<Vector3> placeablePositions =
            floorWorldPositions.ToList().FindAll(e => Vector3.Distance(spawnPosition, e) <= detectionRadius);

        if (placeablePositions.Count > detectionRadius * 4 || presetPattern != null)
        {
            var pattern = presetPattern == null ? enemyPlacerPatterns[Random.Range(0, enemyPlacerPatterns.Count)] : presetPattern;
            PlaceCharacterPattern(pattern, placeablePositions);
        }
    }

    void PlaceCharacterPattern(CharacterPlacerPattern pattern, List<Vector3> placeablePositions)
    {
        foreach (var enemy in pattern.Enemies)
        {
            var placed = Instantiate(enemy, placeablePositions[Random.Range(0, placeablePositions.Count)], Quaternion.identity, transform);
            placedEnemies.Add(placed);
        }
    }
}

[System.Serializable]
public class CharacterPlacerPattern
{
    public string Name = "Enemy Pattern";
    public List<Enemy> Enemies = new List<Enemy>();
}

public static class EnemyPlaceExtension
{
    public static Vector3[] ToWorldArray(this Vector2Int[] original, Tilemap tilemap)
    {
        Vector3[] output = new Vector3[original.Length];
        for (int i = 0; i < output.Length; i++)
        {
            output[i] = tilemap.CellToWorld((Vector3Int)original[i]);
        }
        return output;
    }
}

