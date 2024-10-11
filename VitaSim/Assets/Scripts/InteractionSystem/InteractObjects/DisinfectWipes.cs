using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisinfectWipes : MonoBehaviour, IInteractable {
    public string InteractionPrompt => "Take disinfect wipes";
    private InteractionPromptUI promptUI;

    private void Awake() {
        promptUI = GetComponentInChildren<InteractionPromptUI>();
    }

    public bool IsInteractable {
        get {
            return HospitalManager.Instance.State == GameState.DisinfectWipes;
        }
    }

    public bool Interact(Interactor interactor) {
        HospitalManager.Instance.UpdateGameState(GameState.CleanFacility);
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