using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public string InteractionPrompt { get; }
    public bool Interact(Interactor interactor);
    public bool IsInteractable { get; }
    void ShowPrompt();
    void HidePrompt();
}