using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteUIController : MonoBehaviour {
    public static NoteUIController Instance;

    private string[] pages;
    private Sprite[] pageImages;
    private int currentPageIndex = 0;

    private void Awake() {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void OpenNoteUI(string[] pages, Sprite[] pageImages) {
        this.pages = pages;
        this.pageImages = pageImages;
        currentPageIndex = 0;
        UpdateNoteContent();
        UIManager.Instance.ShowNoteUI();
    }

    private void UpdateNoteContent() {
        string currentPageText = pages[currentPageIndex];
        Sprite currentPageImage = null;

        if (pageImages != null && pageImages.Length+1 > currentPageIndex) {
            currentPageImage = pageImages[currentPageIndex];
        }

        UIManager.Instance.UpdateNoteContent(currentPageText, currentPageImage);
    }
 

    private void Update() {
        if (UIManager.Instance.noteUIPanel.activeSelf) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                UIManager.Instance.HideNoteUI();

                FindObjectOfType<Interactor>().enabled = true;

                HospitalManager.Instance.UpdateGameState(GameState.OpenWindows);
            }
            else if (Input.GetKeyDown(KeyCode.T)) {
                NextPage();
            }
            else if (Input.GetKeyDown(KeyCode.Z)) {
                PreviousPage();
            }
        }
    }

    public void NextPage() {
        if (currentPageIndex < pages.Length - 1) {
            currentPageIndex++;
            UpdateNoteContent();
        }
    }

    public void PreviousPage() {
        if (currentPageIndex > 0) {
            currentPageIndex--;
            UpdateNoteContent();
        }
    }
}
