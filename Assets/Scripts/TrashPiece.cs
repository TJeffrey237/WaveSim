using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TrashPiece : MonoBehaviour
{
    public float minWeight = 1f;
    public float maxWeight = 5f;
    public float weight;

    [HideInInspector]
    public TrashZone zone;

    private bool collected;
    private Collider cachedCollider;
    private Renderer cachedRenderer;

    void Awake()
    {
        cachedCollider = GetComponent<Collider>();
        cachedRenderer = GetComponentInChildren<Renderer>();
        if (cachedCollider != null)
        {
            cachedCollider.isTrigger = true;
        }

        weight = Random.Range(minWeight, maxWeight);
    }

    public void Initialize(TrashZone sourceZone)
    {
        zone = sourceZone;
    }

    void OnTriggerEnter(Collider other)
    {
        if (collected)
            return;

        BoatController boat = other.GetComponent<BoatController>();
        if (boat == null)
            return;

        Collect(boat);
    }

    private void Collect(BoatController boat)
    {
        if (collected)
            return;

        collected = true;
        boat.AddWeight(weight);
        boat.carriedTrashCount += 1;
        zone.NotifyTrashCollected();

        if (cachedRenderer != null)
            cachedRenderer.enabled = false;

        if (cachedCollider != null)
            cachedCollider.enabled = false;

        Debug.Log($"Picked up trash weighing {weight:F1}. Boat weight is now {boat.weight:F1}.");
    }
}
