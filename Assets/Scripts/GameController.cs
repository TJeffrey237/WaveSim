using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [Header("Gameplay")]
    public BoatController boat;
    public List<TrashZone> trashZones = new List<TrashZone>();
    public UnloadingZone unloadingZone;

    [Header("Trash Setup")]
    public TrashPiece trashPrefab;
    public int trashPerZone = 4;
    public float zoneRadius = 6f;

    [HideInInspector]
    public int totalCollectedTrash;

    [HideInInspector]
    public int totalTrashToClear;
    private int totalDroppedTrash;
    private int zonesCleared;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        if (boat == null)
            boat = FindObjectOfType<BoatController>();

        if (trashPrefab == null)
            Debug.LogWarning("GameController needs a TrashPiece prefab assigned.");

        if (trashZones.Count == 0)
            trashZones.AddRange(FindObjectsOfType<TrashZone>());

        totalTrashToClear = trashZones.Count * trashPerZone;

        foreach (TrashZone zone in trashZones)
        {
            zone.Initialize(this, trashPrefab, trashPerZone, zoneRadius);
        }

        if (unloadingZone == null)
            unloadingZone = FindObjectOfType<UnloadingZone>();

        Debug.Log("Collect trash from the buoy zones and bring it back to the unloading zone.");
    }

    public void NotifyZoneCleared(TrashZone zone)
    {
        zonesCleared++;
        Debug.Log($"Zone cleared: {zone.name} ({zonesCleared}/{trashZones.Count}).");
        CheckGameEnd();
    }

    public void NotifyTrashCollected()
    {
        totalCollectedTrash++;
        Debug.Log($"Trash collected: {totalCollectedTrash}/{totalTrashToClear}.");
    }

    public void NotifyTrashDropped(int droppedCount)
    {
        totalDroppedTrash += droppedCount;
        Debug.Log($"Dropped off {droppedCount} trash piece(s). Total dropped: {totalDroppedTrash}/{totalTrashToClear}.");
        CheckGameEnd();
    }

    private void CheckGameEnd()
    {
        if (totalDroppedTrash >= totalTrashToClear && zonesCleared >= trashZones.Count)
        {
            Debug.Log("All dock zones are cleared and all trash has been unloaded. You win!");
        }
    }
}
