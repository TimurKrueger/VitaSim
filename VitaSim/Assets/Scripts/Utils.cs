using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shared
{
    /// <summary>
    /// Helper class for common functionality
    /// </summary>
    public static class Utils
    {
        #region Show cursor

        public static void EnablePlayerCamera()
        {
            //Object.FindObjectOfType<CinemachinePOVExtension>().active = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public static void DisablePlayerCamera()
        {
            //Object.FindObjectOfType<CinemachinePOVExtension>().active = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        #endregion

        #region Lock player

        public static void EnablePlayerMovement()
        {
           // Object.FindObjectOfType<Player.PlayerController>().ResumeMovement();
        }

        public static void DisablePlayerMovement()
        {
           // Object.FindObjectOfType<Player.PlayerController>().PauseMovement();
        }

        #endregion

        #region Pause

        public static void PauseGame()
        {
            Time.timeScale = 0f;
        }

        public static void UnpauseGame()
        {
            Time.timeScale = 1f;
        }

        #endregion
/*
        #region Dont Destroy On Load

        static List<GameObject> permanents = new List<GameObject>();

        public static void MakePermanent(GameObject obj)
        {
            permanents.Add(obj);
            GameObject.DontDestroyOnLoad(obj);
        }

        public static void ClearPermanents()
        {
            while(permanents.Count > 0)
            {
                GameObject.Destroy(permanents[0]);
                permanents.RemoveAt(0);
            }

            permanents.Clear();
        }

        #endregion

        #region Scene Management

        public static string LevelToFloor(GameManager.Level level)
        {
            switch (level)
            {
                case GameManager.Level.Lobby:
                    return "0";
                case GameManager.Level.Sewers:
                    return "-27";
                case GameManager.Level.Asylum:
                    return "56";
                case GameManager.Level.ThePast:
                    return "130";
                case GameManager.Level.Labratory:
                    return "-10";
                case GameManager.Level.Penthouse:
                    return "PH";
                default:
                    return "NA";
            }
        }

        public static void RestartLevel()
        {
            ClearLevelStatics();

            GameManager.Instance.ReloadLevel();
            //GameObject.FindObjectOfType<LevelManager>().
        }

        public static void ClearLevelStatics()
        {            
            // Asylum
            Asylum.AsylumManager.Instance = null;
            Asylum.FieldOfView.Instance = null;

            // Past
            Asylum.FieldOfView_FirstHall.Instance = null;
            Asylum.FieldOfView_K.Instance = null;
            Past.GameManagerTrigger.Instance = null;

            // Lab
            Asylum.FieldOfView_B.Instance = null;

            // Sewers
            //Sewers.SewerManager.Inst = null;  // Wiped via LevelManager.Inst = null
            Sewers.Wayfinding.WayfindingManager.Instance = null;

            LevelManager.Inst = null;
        }

        public static void RestartGame()
        {
            Utils.EnablePlayerCamera();
            Utils.EnablePlayerMovement();

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            // TODO: Reset all statics
            ClearLevelStatics();

            // Shared
            Shared.GameManager.Instance = null;
            Shared.UIInteractPrompt.Instance = null;
            Shared.LevelManager.Inst = null;
            Shared.Player.PlayerController.Instance = null;
            SoundManager.Instance = null;
            Shared.UIManager.Instance = null;

            ClearPermanents();
            SceneManager.LoadScene("MainMenu");
        }

        #endregion
    }*/
}
}