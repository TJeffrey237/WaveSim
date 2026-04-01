using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI weightText;
    public TextMeshProUGUI holdingText;
    public TextMeshProUGUI collectedText;

    private GameController gameController;
    private BoatController boat;

    void Start()
    {
        gameController = GameController.Instance;
        if (gameController == null)
        {
            Debug.LogWarning("GameUI: No GameController instance found.");
            return;
        }

        boat = gameController.boat;
        if (boat == null)
        {
            boat = FindObjectOfType<BoatController>();
            if (boat != null)
            {
                gameController.boat = boat;
            }
            else
            {
                Debug.LogWarning("GameUI: No BoatController found in scene.");
            }
        }
    }

    void Update()
    {
        if (gameController == null || boat == null)
            return;

        if (weightText != null)
            weightText.text = $"Weight: {boat.weight:F1}";

        if (holdingText != null)
            holdingText.text = $"Holding: {boat.carriedTrashCount}";

        if (collectedText != null)
            collectedText.text = $"Dropped Off: {gameController.DroppedTrashCount}/{gameController.totalTrashToClear}";
    }
}
