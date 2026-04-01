using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Text weightText;
    public Text holdingText;
    public Text collectedText;

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
            Debug.LogWarning("GameUI: No BoatController reference set in GameController.");
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
            collectedText.text = $"Collected: {gameController.totalCollectedTrash}/{gameController.totalTrashToClear}";
    }
}
