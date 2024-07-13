using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractButtonController : MonoBehaviour
{
    public static InteractButtonController Instance;

    public Button interactButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        interactButton.gameObject.SetActive(false);
        interactButton.onClick.AddListener(OnInteractButtonPressed);
    }

    public void ShowButton()
    {
        interactButton.gameObject.SetActive(true);
    }

    public void HideButton()
    {
        interactButton.gameObject.SetActive(false);
    }

    private void OnInteractButtonPressed()
    {
        if (FirstPersonController.Instance != null)
        {
            FirstPersonController.Instance.Interact();
        }
    }
}
