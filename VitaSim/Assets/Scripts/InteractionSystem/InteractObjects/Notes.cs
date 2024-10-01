using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes: MonoBehaviour, IInteractable {
    public string InteractionPrompt => "Article about viruses";
    private InteractionPromptUI promptUI;

    private void Awake() {
        promptUI = GetComponentInChildren<InteractionPromptUI>();
    }

    public bool IsInteractable {
        get {
            return HospitalManager.Instance.State == GameState.ReadAllNotes;
        }
    }

    public bool Interact(Interactor interactor) {
        if (!IsInteractable) {
            Debug.Log("I don't need this right now.");
            return false;
        }

        UIManager.Instance.ShowNoteUI();
        interactor.enabled = false;

        HospitalManager.Instance.UpdateGameState(GameState.PickUpSanitizerSprayCan);
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
