using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hostiles : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    { 
        SceneManager.LoadScene("ClearViruses");
        return true;

        // interactor is the player, access here to make checks if the player has everything to be able to interact
    }
}
