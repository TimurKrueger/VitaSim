using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NoteUIController : MonoBehaviour {

    void Update() {
        if (UIManager.Instance.noteUIPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape)) {
            // Hide the note UI
            UIManager.Instance.HideNoteUI();

            // Re-enable player controls
            FindObjectOfType<Interactor>().enabled = true;
            // If you disabled player movement, re-enable it here

            // Start the next sequence
            HospitalManager.Instance.UpdateGameState(GameState.PickUpSanitizerSprayCan);
        }
    }
}
