using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

    public class UIManager : MonoBehaviour
    {
        [Header("Menu Handling")]
        public static UIManager Instance;

        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject hudCanvas;
        [SerializeField] private TextMeshProUGUI taskText;
        [SerializeField] private TextMeshProUGUI dailyTaskText;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        public void UpdateTaskText(string task)
        {
            taskText.text = task;
        }

        public void UpdateDailyTaskText(string task)
        {
            dailyTaskText.text = task;
        }
    }
