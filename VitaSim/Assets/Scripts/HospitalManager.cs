using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum GameState {
   TalkToDoctor,
   DisinfectYourHands,
   FindMedication,
   GiveMedicationToPatient,
   ReportFindingsToDoctor,
   ReadAllNotes,
   OpenWindows,
   DisinfectWipes,
   CleanFacility,
   PickUpSanitizerSprayCan,
   DestroyAllViruses,
   SubmitReportToChiefDoctor,
   CompleteLevel
}

public class HospitalManager : MonoBehaviour, LevelManager {
    // Game State Management
    public static HospitalManager Instance { get; private set; }
    [SerializeField] public GameState State;

    // OUtbreak Management
    [SerializeField] public GameObject outbreakHUD;
    [SerializeField] public HealthBar healthBar;
    public float outbreakPercentage = 0f;
    public float outbreakIncreaseRate = 10f;
    private bool outbreakActive = false;

    // Virus Management
    [SerializeField] public GameObject covid;
    [SerializeField] public GameObject norovirus;
    [SerializeField] public GameObject influenza_1;
    [SerializeField] public GameObject influenza_2;
    [SerializeField] public GameObject influenzaCanvas_1;
    [SerializeField] public GameObject influenzaCanvas_2;
    [SerializeField] public GameObject sprayCan;
    [SerializeField] public GameObject disinfectWipe;
    public int totalVirusCount = 2;
    private int remainingVirusCount;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
        } else {
             Instance = this;
        }
    }
  
    private void Start() {
        StartLevel(null);
    }

    void Update() {
        if (State == GameState.OpenWindows ||
            State == GameState.DisinfectWipes ||
            State == GameState.CleanFacility ||
            State == GameState.PickUpSanitizerSprayCan || 
            State == GameState.DestroyAllViruses) {

            if(!outbreakActive) {
                outbreakActive = true;
            }

            IncreaseOutbreakOverTime();
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
            case GameState.OpenWindows:
                HandleOpenWindows();
                break;
            case GameState.DisinfectWipes:
                HandlePickupDisinfectWipes();
                break;
            case GameState.CleanFacility:
                HandleCleanFacility();
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

    private void HandleTalkToDoctor() {
        UIManager.Instance.UpdateTaskText("Talk to the doctor");
        UIManager.Instance.UpdateDailyTaskText("Task 1/13"); 
        
        Transform newTarget = GameObject.Find("Doctor").transform;
        NavigationPath.Instance.SetNewTarget(newTarget);
    }

    private void HandleDisinfectYourHands() {
        UIManager.Instance.UpdateTaskText("Disinfect your hands");
        UIManager.Instance.UpdateDailyTaskText("Task 2/13");

        Transform newTarget = GameObject.Find("SanitizerDispenser").transform;
        NavigationPath.Instance.SetNewTarget(newTarget);
    }

    private void HandleFindMedication() {
        UIManager.Instance.UpdateTaskText("Find the medication");
        UIManager.Instance.UpdateDailyTaskText("Task 3/13");

        Transform newTarget = GameObject.Find("MedicalKit").transform;
        NavigationPath.Instance.SetNewTarget(newTarget);
    }

    private void HandleGiveMedicationToPatient() {
        UIManager.Instance.UpdateTaskText("Cure the patient");
        UIManager.Instance.UpdateDailyTaskText("Task 4/13");

        Transform newTarget = GameObject.Find("Patient_1").transform;
        NavigationPath.Instance.SetNewTarget(newTarget);
    }

    private void HandleReportFindingsToDoctor() {
        UIManager.Instance.UpdateTaskText("Report to doctor");
        UIManager.Instance.UpdateDailyTaskText("Task 5/13");

        Transform newTarget = GameObject.Find("Doctor").transform;
        NavigationPath.Instance.SetNewTarget(newTarget);
    }

    private void HandleReadAllNotes() {
        UIManager.Instance.UpdateTaskText("Read the article");
        UIManager.Instance.UpdateDailyTaskText("Task 6/13");

        Transform newTarget = GameObject.Find("Note").transform;
        NavigationPath.Instance.SetNewTarget(newTarget);
    }
    private void HandleOpenWindows() {
        UIManager.Instance.UpdateTaskText("Open the window in the lobby");
        UIManager.Instance.UpdateDailyTaskText("Task 7/13");

        remainingVirusCount = totalVirusCount;
        healthBar.UpdateHealth(outbreakPercentage);
        outbreakHUD.SetActive(true);
        covid.SetActive(true);
        Transform newTarget = GameObject.Find("Window").transform;
        NavigationPath.Instance.SetNewTarget(newTarget);
    }

    private void HandlePickupDisinfectWipes() {
        UIManager.Instance.UpdateTaskText("Pick up the disinfect wipes");
        UIManager.Instance.UpdateDailyTaskText("Task 8/13");

        covid.SetActive(false);
        Transform newTarget = GameObject.Find("DisinfectWipes").transform;
        NavigationPath.Instance.SetNewTarget(newTarget);
    }

    private void HandleCleanFacility() {
        UIManager.Instance.UpdateTaskText("Clean the chair handle");
        UIManager.Instance.UpdateDailyTaskText("Task 9/13");

        norovirus.SetActive(true);
        disinfectWipe.SetActive(true);
        Transform newTarget = GameObject.Find("Chair").transform;
        NavigationPath.Instance.SetNewTarget(newTarget);
    }

    private void HandlePickUpSanitizerSprayCan() {
        UIManager.Instance.UpdateTaskText("Pick up the sanitizer spray can");
        UIManager.Instance.UpdateDailyTaskText("Task 10/13");

        norovirus.SetActive(false);
        disinfectWipe.SetActive(false);
        Transform newTarget = GameObject.Find("SanitizerCan").transform;
        NavigationPath.Instance.SetNewTarget(newTarget);
    }

    private void HandleDestroyAllViruses() {
        UIManager.Instance.UpdateTaskText("Eliminate remaining viruses");
        UIManager.Instance.UpdateDailyTaskText("Task 11/13");


        sprayCan.SetActive(true);
        influenzaCanvas_1.SetActive(true);
        influenzaCanvas_2.SetActive(true);
        influenza_1.SetActive(true);
        influenza_2.SetActive(true);
    }

    private void HandleSubmitReportToChiefDoctor() {
        UIManager.Instance.UpdateTaskText("Report to doctor");
        UIManager.Instance.UpdateDailyTaskText("Task 12/13");

        sprayCan.SetActive(false);
        Transform newTarget = GameObject.Find("Doctor").transform;
        NavigationPath.Instance.SetNewTarget(newTarget);
    }

    private void HandleCompleteLevel() {
        UIManager.Instance.UpdateTaskText("Level complete!");
        UIManager.Instance.UpdateDailyTaskText("13/13");
       // TODO: Start new scenario or level
    }

    void IncreaseOutbreakOverTime() {
        float outbreakIncreasePerSecond = outbreakIncreaseRate / 60f;
        outbreakPercentage += outbreakIncreasePerSecond * Time.deltaTime * 3f;
        outbreakPercentage = Mathf.Clamp(outbreakPercentage, 0f, 100f);
        healthBar.UpdateHealth(outbreakPercentage / 100f);
    }

    public void VirusDestroyed() {
        remainingVirusCount--;
        CheckWinCondition();
    }

    void CheckGameOver() {
        if (outbreakPercentage >= 100f) {
            Debug.Log("Game Over! Outbreak reached 100%.");
            State = GameState.ReadAllNotes;
        }
    }

    void CheckWinCondition() {
        if (remainingVirusCount <= 0 && outbreakPercentage < 100f) {
            outbreakHUD.SetActive(false);
            sprayCan.SetActive(false);
            UpdateGameState(GameState.SubmitReportToChiefDoctor);
        }
    }
}
