using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanitizerSprayCan : MonoBehaviour, IInteractable {
    public string InteractionPrompt => "Pick up sanitizer spray can";
    private InteractionPromptUI promptUI;

    private void Awake() {
        promptUI = GetComponentInChildren<InteractionPromptUI>();
    }

    public bool IsInteractable {
        get {
            return HospitalManager.Instance.State == GameState.PickUpSanitizerSprayCan;
        }
    }

    public bool Interact(Interactor interactor) {
        HospitalManager.Instance.UpdateGameState(GameState.DestroyAllViruses);
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