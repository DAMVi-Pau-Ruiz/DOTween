
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Raycast Settings")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float rayDistance = 5f;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference interactAction;

    private IInteractable currentInteractable;
    private IHighlightable currentHighlight;

    private void Awake()
    {
        if (playerCamera == null)
        {
            playerCamera = GetComponentInChildren<Camera>();
        }
    }

    private void OnEnable()
    {
        interactAction.action.Enable();
    }

    private void OnDisable()
    {
        interactAction.action.Disable();
    }

    private void Update()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        IInteractable newInteractable = null;
        IHighlightable newHighlight = null;

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            newInteractable = hit.collider.GetComponentInParent<IInteractable>();
            newHighlight = hit.collider.GetComponentInParent<IHighlightable>();
        }

        if (currentHighlight != newHighlight)
        {
            currentHighlight?.ShowHighlight(false);
            newHighlight?.ShowHighlight(true);

            currentHighlight = newHighlight;
        }

        currentInteractable = newInteractable;

        if (interactAction.action.WasPressedThisFrame())
        {
            currentInteractable?.Interact();
        }
    }

    private void OnDrawGizmos()
    {
        if (playerCamera == null) return;

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray.origin, ray.direction * rayDistance);
    }
}
