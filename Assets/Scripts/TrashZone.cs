using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TrashZone : MonoBehaviour
{
    public int trashCount = 4;
    public float radius = 6f;
    public TrashPiece trashPrefab;

    [HideInInspector]
    public int remainingTrash;

    private GameController gameController;
    private bool clearedNotified;

    public void Initialize(GameController controller, TrashPiece prefab, int count, float radius)
    {
        gameController = controller;
        trashPrefab = prefab;
        trashCount = count;
        this.radius = radius;
        SpawnTrash();
    }

    private void Start()
    {
        if (gameController == null && GameController.Instance != null)
        {
            gameController = GameController.Instance;
        }

        // Only self-spawn if no GameController is managing this zone.
        if (gameController == null && trashPrefab != null && remainingTrash == 0)
        {
            SpawnTrash();
        }
    }

    private void SpawnTrash()
    {
        remainingTrash = trashCount;
        for (int i = 0; i < trashCount; i++)
        {
            Vector2 circle = Random.insideUnitCircle * radius;
            Vector3 spawnPosition = transform.position + new Vector3(circle.x, 0.5f, circle.y);
            TrashPiece piece = Instantiate(trashPrefab, spawnPosition, Quaternion.identity, transform);
            piece.Initialize(this);
        }
    }

    public void NotifyTrashCollected()
    {
        remainingTrash = Mathf.Max(0, remainingTrash - 1);

        if (remainingTrash == 0 && !clearedNotified)
        {
            clearedNotified = true;
            gameController?.NotifyZoneCleared(this);
        }
    }
}
