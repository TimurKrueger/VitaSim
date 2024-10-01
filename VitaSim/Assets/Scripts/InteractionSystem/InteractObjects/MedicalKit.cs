using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicalKit: MonoBehaviour, IInteractable {
    public string InteractionPrompt => "Pick up medication";
    private InteractionPromptUI promptUI;

    private void Awake() {
        promptUI = GetComponentInChildren<InteractionPromptUI>();
    }

    public bool IsInteractable {
        get {
            return HospitalManager.Instance.State == GameState.FindMedication;
        }
    }

    public bool Interact(Interactor interactor) {
        if (!IsInteractable) {
            Debug.Log("I don't need this right now.");
            return false;
        }

        Inventory.Instance.hasMedicalKit = true;
        HospitalManager.Instance.UpdateGameState(GameState.GiveMedicationToPatient); 
        Destroy(gameObject); 
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
