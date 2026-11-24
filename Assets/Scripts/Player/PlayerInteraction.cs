
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Raycast Settings")]
    [SerializeField] private float rayDistance = 5f;

    [Header("Interaction")]
    [SerializeField] private Scenario2 scenario;
    [SerializeField] private Scenario2 scenario2;

    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            if (hit.collider.CompareTag("Button"))
            {
                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    scenario2.StartCase1();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * rayDistance);
    }
}
