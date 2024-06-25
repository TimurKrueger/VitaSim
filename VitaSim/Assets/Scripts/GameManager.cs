using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Level
    {
        Puzzle, // There are 10 cards, with medical equipment, always 2 cards are chosen and need to match
        Sorting, // Each medical equipment needs to be sorted to the according case
        Cleaning, // On the image there are 3 accidents which need to be cleaned up
        Cupboard, // Equipment has been placed at wrong places, clean the cupboard by putting them to the correct places
        Training // This involves training of new skills, tbd
    }

    public struct PlayerData
    {
        public static readonly int objectiveCount = 5;

        public Level currentLevel;

        public bool completedPuzzle;
        public bool completedSorting;
        public bool completedCleaning;
        public bool completedCupboard;
        public bool completedTraining;
    }

    public Level CurrentLevel => playerData.currentLevel;

    [SerializeField] private PlayerData playerData;

    [SerializeField] private Transform player;
    [SerializeField] private Transform uiManager;
    [SerializeField] private Transform soundManager;
    [SerializeField] private LevelManager levelManager;

    [SerializeField] public static GameManager Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else if (Instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        Application.backgroundLoadingPriority = ThreadPriority.BelowNormal;

    }

    public void LoadLevel(string name)
    {
        LevelManager.Inst = null;
    }
}
