using UnityEngine;
using UnityEngine.InputSystem;
using CorporateHunger.Core;

namespace CorporateHunger.Player
{
    
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float arrivalThreshold = 0.1f;

        private Rigidbody2D rb;
        private PlayerInputActions inputActions;
        private Camera mainCamera;

        private Vector2 targetPosition;
        private bool hasTarget = false;
        private IInteractable pendingInteractable;

        [SerializeField] private float approachDistance = 1f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            inputActions = new PlayerInputActions();
            mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            inputActions.Player.Enable();
            inputActions.Player.Click.performed += OnClickPerformed;
        }

        private void OnDisable()
        {
            inputActions.Player.Click.performed -= OnClickPerformed;
            inputActions.Player.Disable();
        }

        private void OnClickPerformed(InputAction.CallbackContext context)
        {
            Vector2 screenPosition = inputActions.Player.PointerPosition.ReadValue<Vector2>();
            Vector2 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);

            Collider2D hit = Physics2D.OverlapPoint(worldPosition);

            if (hit != null && hit.GetComponent<IInteractable>() != null)
            {
                pendingInteractable = hit.GetComponent<IInteractable>();

                // Hitung titik pendekatan: sedikit di depan object,
                // ke arah Player, supaya tidak nyangkut di Collider object.
                Vector2 objectPosition = hit.transform.position;
                Vector2 directionFromObject = ((Vector2)rb.position - objectPosition).normalized;
                targetPosition = objectPosition + directionFromObject * approachDistance;
            }
            else
            {
                pendingInteractable = null;
                targetPosition = worldPosition;
            }

            hasTarget = true;
        }

        private void FixedUpdate()
        {
            if (!hasTarget) return;

            Vector2 currentPosition = rb.position;
            float distance = Vector2.Distance(currentPosition, targetPosition);

            if (distance <= arrivalThreshold)
            {
                // Sudah sampai tujuan, berhenti.
                hasTarget = false;

                // Kalau ada interactable yang ditunggu, trigger sekarang.
                if (pendingInteractable != null && pendingInteractable.CanInteract())
                {
                    pendingInteractable.Interact();
                }
                pendingInteractable = null;
                return;
            }

            Vector2 newPosition = Vector2.MoveTowards(
                currentPosition,
                targetPosition,
                moveSpeed * Time.fixedDeltaTime
            );

            rb.MovePosition(newPosition);
        }
    }
}