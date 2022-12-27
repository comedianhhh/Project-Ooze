using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrapPlacer : MonoBehaviour
{
    [SerializeField] Transform TrapParent;

    [Header("References")]
    [SerializeField] List<GameObject> traps = new List<GameObject>();


    [Header("Settings")]
    [SerializeField] float detectionRadius = 5;

    [Header("Data")]
    [SerializeField] List<GameObject> placedTraps = new List<GameObject>();
    [SerializeField] public List<Vector3> placedPositions = new List<Vector3>();

    public void PlaceEveryRoom(Vector3[] roomPositions, Vector3[] floorWorldPositions)
    {
        placedPositions.Clear();
        if (roomPositions.Length == 0 || floorWorldPositions.Length == 0) return;

        placedPositions.Clear();

        for (int i = 0; i < placedTraps.Count; i++)
            if (placedTraps[i])
            {
                DestroyImmediate(placedTraps[i].gameObject);
            }
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
                placedPositions.Add(pos);
                var traps = Instantiate(trap, pos+new Vector3(0.5f,0.5f,0), Quaternion.identity, TrapParent);
                placedTraps.Add(traps);
            }
        }
    }

}
