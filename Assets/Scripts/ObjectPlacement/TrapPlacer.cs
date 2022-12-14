using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrapPlacer : MonoBehaviour
{

    [Header("References")]
    [SerializeField] List<GameObject> traps = new List<GameObject>();

    [Header("Settings")]
    [SerializeField] float detectionRadius = 5;

    [Header("Data")]
    [SerializeField] List<GameObject> placedTraps = new List<GameObject>();

    public void PlaceEveryRoom(Vector3[] roomPositions, Vector3[] floorWorldPositions)
    {
        if (roomPositions.Length == 0 || floorWorldPositions.Length == 0) return;

        for (int i = 0; i < placedTraps.Count; i++)
            DestroyImmediate(placedTraps[i].gameObject);
        placedTraps.Clear();

        for (int i = 0; i < roomPositions.Length; i++)
        {
            if (i == roomPositions.Length - 1)
                Place(roomPositions[i], floorWorldPositions);
            else
                Place(roomPositions[i], floorWorldPositions);
        }

    }
    public void Place(Vector3 spawnPosition, Vector3[] floorWorldPositions)
    {
        List<Vector3> placeablePositions =
            floorWorldPositions.ToList().FindAll(e => Vector3.Distance(spawnPosition, e) <= detectionRadius);

        if (placeablePositions.Count > detectionRadius * 4)
        {
            foreach (var trap in traps)
            {
                var pos = placeablePositions[Random.Range(0, placeablePositions.Count)];
                var traps = Instantiate(trap, pos+new Vector3(0.5f,0.5f,0), Quaternion.identity, transform);
                placedTraps.Add(traps);
            }
        }
    }

}
