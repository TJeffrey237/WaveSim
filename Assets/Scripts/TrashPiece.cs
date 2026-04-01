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
    private Collider[] cachedColliders;
    private Renderer[] cachedRenderers;

    void Awake()
    {
        cachedColliders = GetComponentsInChildren<Collider>();
        cachedRenderers = GetComponentsInChildren<Renderer>();

        foreach (Collider c in cachedColliders)
        {
            c.isTrigger = true;
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

        BoatController boat = other.GetComponentInParent<BoatController>();
        if (boat == null && other.attachedRigidbody != null)
        {
            boat = other.attachedRigidbody.GetComponent<BoatController>();
        }

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

        foreach (Renderer r in cachedRenderers)
        {
            if (r != null)
                r.enabled = false;
        }

        foreach (Collider c in cachedColliders)
        {
            if (c != null)
                c.enabled = false;
        }

        Debug.Log($"Picked up trash weighing {weight:F1}. Boat weight is now {boat.weight:F1}.");
    }
}
