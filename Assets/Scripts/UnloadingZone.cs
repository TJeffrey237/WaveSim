using UnityEngine;

[RequireComponent(typeof(Collider))]
public class UnloadingZone : MonoBehaviour
{
    public string zoneName = "Unloading Zone";

    void Awake()
    {
        Collider col = GetComponent<Collider>();
        if (col != null)
            col.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        BoatController boat = other.GetComponent<BoatController>();
        if (boat == null)
            return;

        int dropped = boat.DropAllTrash();
        if (dropped > 0)
        {
            GameController.Instance?.NotifyTrashDropped(dropped);
            Debug.Log($"{zoneName}: Dropped off {dropped} trash piece(s).");
        }
    }
}
