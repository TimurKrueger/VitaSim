using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
   TalkToDoctor,
   DisinfectYourHands,
   FindMedication,
   GiveMedicationToPatient,
   ReportFindingsToDoctor,
   ReadAllNotes,
   PickUpSanitizerSprayCan,
   DestroyAllViruses,
   SubmitReportToChiefDoctor,
   CompleteLevel
}

public class HospitalManager : MonoBehaviour, LevelManager
{ 
    public static HospitalManager Instance { get; private set; }

    [SerializeField] public GameState State;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start() {
        StartLevel(null);
    }

    public void UpdateGameState(GameState newState) {
        State = newState;

        switch (newState) {
            case GameState.TalkToDoctor:
                HandleTalkToDoctor();
                break;
            case GameState.DisinfectYourHands:
                HandleDisinfectYourHands();
                break;
            case GameState.FindMedication:
                HandleFindMedication();
                break;
            case GameState.GiveMedicationToPatient:
                HandleGiveMedicationToPatient();
                break;
            case GameState.ReportFindingsToDoctor:
                HandleReportFindingsToDoctor();
                break;
            case GameState.ReadAllNotes:
                HandleReadAllNotes();
                break;
            case GameState.PickUpSanitizerSprayCan:
                HandlePickUpSanitizerSprayCan();
                break;
            case GameState.DestroyAllViruses:
                HandleDestroyAllViruses();
                break;
            case GameState.SubmitReportToChiefDoctor:
                HandleSubmitReportToChiefDoctor();
                break;
            case GameState.CompleteLevel:
                HandleCompleteLevel();
                break;
            default:
                Debug.LogWarning("Unhandled game state: " + newState);
                break;
        }
    }

    public void StartLevel(Action<int> OnFinish) {
        UpdateGameState(GameState.TalkToDoctor);
    }

    public void CompleteLevel() {
        Debug.Log("Level Completed!");
    }

    private void HandleTalkToDoctor() {
        UIManager.Instance.UpdateTaskText("Talk to the doctor.");
        UIManager.Instance.UpdateDailyTaskText("Task 1/10: Talk to the doctor.");
    }

    private void HandleDisinfectYourHands() {
        UIManager.Instance.UpdateTaskText("Disinfect your hands.");
        UIManager.Instance.UpdateDailyTaskText("Task 2/10: Disinfect your hands.");
    }

    private void HandleFindMedication() {
        UIManager.Instance.UpdateTaskText("Find the medication.");
        UIManager.Instance.UpdateDailyTaskText("Task 3/10: Find the medication.");
    }

    private void HandleGiveMedicationToPatient() {
        UIManager.Instance.UpdateTaskText("Give medication to the patient.");
        UIManager.Instance.UpdateDailyTaskText("Task 4/10: Give medication to the patient.");
    }

    private void HandleReportFindingsToDoctor() {
        UIManager.Instance.UpdateTaskText("Report findings to the doctor.");
        UIManager.Instance.UpdateDailyTaskText("Task 5/10: Report findings to the doctor.");
    }

    private void HandleReadAllNotes() {
        UIManager.Instance.UpdateTaskText("Read all notes.");
        UIManager.Instance.UpdateDailyTaskText("Task 6/10: Read all notes.");
    }

    private void HandlePickUpSanitizerSprayCan() {
        UIManager.Instance.UpdateTaskText("Pick up the sanitizer spray can.");
        UIManager.Instance.UpdateDailyTaskText("Task 7/10: Pick up the sanitizer spray can.");
    }

    private void HandleDestroyAllViruses() {
        UIManager.Instance.UpdateTaskText("Destroy all visible viruses.");
        UIManager.Instance.UpdateDailyTaskText("Task 8/10: Destroy all viruses.");
    }

    private void HandleSubmitReportToChiefDoctor() {
        UIManager.Instance.UpdateTaskText("Submit report to the chief doctor.");
        UIManager.Instance.UpdateDailyTaskText("Task 9/10: Submit report to the chief doctor.");

    }

    private void HandleCompleteLevel() {
        UIManager.Instance.UpdateTaskText("Level complete! All tasks completed.");
        UIManager.Instance.UpdateDailyTaskText("Task 10/10: Level completed.");
        CompleteLevel();
    }
}
