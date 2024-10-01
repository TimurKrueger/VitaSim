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
    [SerializeField] private Camera playerCamera;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;

    private IInteractable _currentInteractable;

    private void Start()
    {
        if (_interactionButton != null)
        {
            _interactionButton.onClick.AddListener(OnInteractionButtonPressed);
        }
    }

    private void Update() {
        _numFound = Physics.OverlapSphereNonAlloc(
            _interactionPoint.position,
            _interactionPointRadius,
            _colliders,
            _interactableMask
        );

        if (_numFound > 0) {
            Debug.Log("Interactable found");
            IInteractable interactable = _colliders[0].GetComponentInParent<IInteractable>();

            if (interactable != null && interactable.IsInteractable) {
                if (_currentInteractable != interactable) {
                    _currentInteractable?.HidePrompt();
                    _currentInteractable = interactable;
                    _currentInteractable.ShowPrompt();
                }

                _interactionButton.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E)) {
                    _currentInteractable.Interact(this);
                }
            }
            else {
                if (_currentInteractable != null) {
                    HideInteractionUI();
                }
            }
        }
        else {
            if (_currentInteractable != null) {
                HideInteractionUI();
            }
        }
    }

    private void HideInteractionUI() {
        _interactionButton.gameObject.SetActive(false);

        if (_currentInteractable != null) {
            _currentInteractable.HidePrompt();
            _currentInteractable = null;
        }
    }

    public void OnInteractionButtonPressed()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

        if (_numFound > 0)
        {
            _currentInteractable = _colliders[0].GetComponentInParent<IInteractable>();

            if (_currentInteractable != null)
            {
                _currentInteractable.Interact(this);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
