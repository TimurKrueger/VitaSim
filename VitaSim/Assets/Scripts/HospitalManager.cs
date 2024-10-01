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
    // Game State Management
    public static HospitalManager Instance { get; private set; }
    [SerializeField] public GameState State;

    // OUtbreak Management
    [SerializeField] public GameObject outbreakHUD;
    [SerializeField] public HealthBar healthBar;
    public float outbreakPercentage = 0f;
    public float outbreakIncreaseRate = 5f;
    private bool outbreakActive = false;

    // Virus Management
    public int totalVirusCount = 5;
    private int remainingVirusCount;
    [SerializeField] public GameObject virus1;
    [SerializeField] public GameObject virus2;
    [SerializeField] public GameObject virus3;
    [SerializeField] public GameObject virus4;
    [SerializeField] public GameObject virus5;

    [SerializeField] public GameObject sprayCan;

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

    void Update() {
        if (State == GameState.DestroyAllViruses) {
            if(!outbreakActive) {
                outbreakActive = true;
            }

            Debug.Log("Outbreak Percentage Before: " + outbreakPercentage);
            IncreaseOutbreakOverTime();
            Debug.Log("Outbreak Percentage After: " + outbreakPercentage);
            CheckGameOver();
        }
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
        UIManager.Instance.UpdateTaskText("Talk to the doctor");
        UIManager.Instance.UpdateDailyTaskText("Task 1/10");
    }

    private void HandleDisinfectYourHands() {
        UIManager.Instance.UpdateTaskText("Disinfect your hands");
        UIManager.Instance.UpdateDailyTaskText("Task 2/10");
    }

    private void HandleFindMedication() {
        UIManager.Instance.UpdateTaskText("Find the medication");
        UIManager.Instance.UpdateDailyTaskText("Task 3/10");
    }

    private void HandleGiveMedicationToPatient() {
        UIManager.Instance.UpdateTaskText("Cure the patient");
        UIManager.Instance.UpdateDailyTaskText("Task 4/10");
    }

    private void HandleReportFindingsToDoctor() {
        UIManager.Instance.UpdateTaskText("Report to doctor");
        UIManager.Instance.UpdateDailyTaskText("Task 5/10");
    }

    private void HandleReadAllNotes() {
        UIManager.Instance.UpdateTaskText("Read the article");
        UIManager.Instance.UpdateDailyTaskText("Task 6/10");
    }

    private void HandlePickUpSanitizerSprayCan() {
        UIManager.Instance.UpdateTaskText("Pick up the sanitizer spray can");
        UIManager.Instance.UpdateDailyTaskText("Task 7/10");
    }

    private void HandleDestroyAllViruses() {
        Debug.Log("START VIRUSES");
        remainingVirusCount = totalVirusCount;
        healthBar.UpdateHealth(outbreakPercentage); 
      
        virus1.SetActive(true);
        virus2.SetActive(true);
        virus3.SetActive(true);
        virus4.SetActive(true);
        virus5.SetActive(true);
        outbreakHUD.SetActive(true);
        sprayCan.SetActive(true);
        UIManager.Instance.UpdateTaskText("Eliminate all viruses");
        UIManager.Instance.UpdateDailyTaskText("Task 8/10");
    }

    private void HandleSubmitReportToChiefDoctor() {
        UIManager.Instance.UpdateTaskText("Report to doctor");
        UIManager.Instance.UpdateDailyTaskText("Task 9/10");

    }

    private void HandleCompleteLevel() {
        UIManager.Instance.UpdateTaskText("Level complete!");
        UIManager.Instance.UpdateDailyTaskText("10/10");
        CompleteLevel();
    }

    void IncreaseOutbreakOverTime() {
        float outbreakIncreasePerSecond = outbreakIncreaseRate / 60f;
        outbreakPercentage += outbreakIncreasePerSecond * Time.deltaTime * 3f;
        outbreakPercentage = Mathf.Clamp(outbreakPercentage, 0f, 100f);
        Debug.Log(outbreakPercentage);
        healthBar.UpdateHealth(outbreakPercentage / 100f);
    }

    public void VirusDestroyed() {
        remainingVirusCount--;
        CheckWinCondition();
    }

    void CheckGameOver() {
        if (outbreakPercentage >= 100f) {
            // TODO
            Debug.Log("Game Over! Outbreak reached 100%.");
        }
    }

    void CheckWinCondition() {
        if (remainingVirusCount <= 0 && outbreakPercentage < 100f) {
            outbreakHUD.SetActive(false);
            sprayCan.SetActive(false);
            UpdateGameState(GameState.CompleteLevel);
        }
    }
}
