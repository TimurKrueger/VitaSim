using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour, IInteractable {
    public string InteractionPrompt => "Clean chair handle";
    private InteractionPromptUI promptUI;

    private void Awake() {
        promptUI = GetComponentInChildren<InteractionPromptUI>();
    }

    public bool IsInteractable {
        get {
            return HospitalManager.Instance.State == GameState.CleanFacility;
        }
    }

    public bool Interact(Interactor interactor) {
        if (!IsInteractable) {
            Debug.Log("I don't need this right now.");
            return false;
        }

        HospitalManager.Instance.UpdateGameState(GameState.PickUpSanitizerSprayCan);
       
        return true;
    }

    public void ShowPrompt() {
        promptUI.Setup(InteractionPrompt, transform);
    }

    public void HidePrompt() {
        if (promptUI != null) {
            promptUI.Close();
        }
    }
}
