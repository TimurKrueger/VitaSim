using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Shared;
using UnityEngine.UI;

namespace Shared
{

    public class UIManager : MonoBehaviour
    {
        [Header("Menu Handling")]
        public static UIManager Instance;

        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject interactPrompt;
        [SerializeField] private GameObject hudCanvas;
        [SerializeField] private TextMeshProUGUI taskText;
        [SerializeField] private bool isGamePaused;
        [SerializeField] private bool isGameOver;


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

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isGamePaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }


        public void PauseGame()
        {
            interactPrompt.SetActive(false);
            hudCanvas.SetActive(false);
            pauseMenu.SetActive(true);
            Utils.DisablePlayerCamera();
            Utils.DisablePlayerMovement();
            Utils.PauseGame();
            isGamePaused = true;
        }

        public void ResumeGame()
        {
            interactPrompt.SetActive(true);
            hudCanvas.SetActive(true);
            pauseMenu.SetActive(false);
            Utils.EnablePlayerCamera();
            Utils.EnablePlayerMovement();
            Utils.UnpauseGame();
            isGamePaused = false;
        }

        public void UpdateTaskText(string task)
        {
            taskText.text = task;
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}