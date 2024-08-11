using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 1.0f;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private Button _interactionButton;
    [SerializeField] private InteractionPromptUI _interactionPromptUI;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;

    private IInteractable _interactable;

    private void Start()
    {
        if (_interactionButton != null)
        {
            _interactionButton.onClick.AddListener(OnInteractionButtonPressed);
        }
    }

    // Maybe I need this to draw gizmos later, or if the button is shown or hidden
    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

        if (_numFound > 0)
        {
            _interactable = _colliders[0].GetComponent<IInteractable>();

            _interactionButton.gameObject.SetActive(true);

            if(_interactable != null)
            {
                if (!_interactionPromptUI.isDisplayed)
                {
                    _interactionPromptUI.Setup(_interactable.InteractionPrompt);
                }
            }
        } else
        {
            _interactionButton.gameObject.SetActive(false);

            if (_interactable != null)
            {
                _interactable = null;
            }

            if (_interactionPromptUI.isDisplayed)
            {
                _interactionPromptUI.Close();
            }
        }
    }

    public void OnInteractionButtonPressed()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

        if (_numFound > 0)
        {
            _interactable = _colliders[0].GetComponent<IInteractable>();

            if (_interactable != null)
            {
                _interactable.Interact(this);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
