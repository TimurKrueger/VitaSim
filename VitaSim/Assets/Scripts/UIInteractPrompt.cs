using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIInteractPrompt : MonoBehaviour
{
    [SerializeField] public static UIInteractPrompt Instance;
    [SerializeField] public TextMeshProUGUI promptText;

    private void Awake()
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

    private void Start()
    {
        //Player.FirstPersonController.Instance.OnFindInteractable += Show;
        //Player.FirstPersonController.Instance.OnLoseInteractable += Hide;
    }

    public void Show(Interactable i)
    {
        promptText.gameObject.SetActive(true);
    }

    public void Hide(Interactable i)
    {
        if (promptText != null)
        {
            promptText.gameObject.SetActive(false);
        }
    }

}
