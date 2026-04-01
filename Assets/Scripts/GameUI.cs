using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI weightText;
    public TextMeshProUGUI holdingText;
    public TextMeshProUGUI collectedText;
    public GameObject winPopup;
    public string mainMenuSceneName = "MainMenu";

    private GameController gameController;
    private BoatController boat;

    void Start()
    {
        gameController = GameController.Instance;

        boat = gameController.boat;
        if (boat == null)
        {
            boat = FindObjectOfType<BoatController>();
            if (boat != null) {
                gameController.boat = boat;
            }
        }

        if (winPopup != null)
            winPopup.SetActive(false);
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

        if (gameController.isGameOver && winPopup != null && !winPopup.activeSelf)
        {
            winPopup.SetActive(true);
        }
    }

    public void OnReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
