using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doctor: MonoBehaviour, IInteractable {
    public string InteractionPrompt => "I have a task for you!";
    public string InteractionPrompt2 => "Thanks for helping me out!";
    public string InteractionPrompt3 => "Good job!";

    private InteractionPromptUI promptUI;

    private void Awake() {
        promptUI = GetComponentInChildren<InteractionPromptUI>();
    }

    public bool IsInteractable {
        get {
            return HospitalManager.Instance.State == GameState.TalkToDoctor ||
                HospitalManager.Instance.State == GameState.ReportFindingsToDoctor ||
                HospitalManager.Instance.State == GameState.SubmitReportToChiefDoctor;
        }
    }

    public bool Interact(Interactor interactor) {
        if(!IsInteractable) {
            return false;
        }

        if (HospitalManager.Instance.State == GameState.TalkToDoctor) {
            HospitalManager.Instance.UpdateGameState(GameState.DisinfectYourHands);
        } else if (HospitalManager.Instance.State == GameState.ReportFindingsToDoctor) {
            HospitalManager.Instance.UpdateGameState(GameState.ReadAllNotes);
        } else if(HospitalManager.Instance.State == GameState.SubmitReportToChiefDoctor) {
            HospitalManager.Instance.UpdateGameState(GameState.CompleteLevel);
        }
        
        return true;
    }

    public void ShowPrompt() {
        Debug.Log(HospitalManager.Instance.State);
        if (HospitalManager.Instance.State == GameState.TalkToDoctor) {
            promptUI.Setup(InteractionPrompt, transform);
        }
        else if (HospitalManager.Instance.State == GameState.ReportFindingsToDoctor) {
            promptUI.Setup(InteractionPrompt2, transform);
        }
        else if (HospitalManager.Instance.State == GameState.SubmitReportToChiefDoctor) {
            promptUI.Setup(InteractionPrompt3, transform);
        }
    }

    public void HidePrompt() {
        promptUI.Close();
    }
}
