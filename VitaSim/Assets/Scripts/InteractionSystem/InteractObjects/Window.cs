using UnityEngine;

public class Window : MonoBehaviour, IInteractable {
    public string InteractionPrompt => "Open Window";
    private InteractionPromptUI promptUI;

    public GameObject windowPane;

    private void Awake() {
        promptUI = GetComponentInChildren<InteractionPromptUI>();
    }

    public bool IsInteractable {
        get {
            return HospitalManager.Instance.State == GameState.OpenWindows;
        }
    }

    public bool Interact(Interactor interactor) {
        if (!IsInteractable) {
            return false;
        }

        windowPane.SetActive(false);

        HospitalManager.Instance.UpdateGameState(GameState.DisinfectWipes);

        return true;
    }

    public void ShowPrompt() {
        if (promptUI != null) {
            promptUI.Setup(InteractionPrompt, transform);
        }
    }

    public void HidePrompt() {
        if (promptUI != null) {
            promptUI.Close();
        }
    }
}
