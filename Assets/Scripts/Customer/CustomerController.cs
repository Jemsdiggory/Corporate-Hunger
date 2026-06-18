using UnityEngine;
using CorporateHunger.Core;

namespace CorporateHunger.Customer
{
    public class CustomerController : MonoBehaviour, IInteractable
    {
        [SerializeField] private CustomerTypeData typeData;
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float arrivalThreshold = 0.1f;

        private CustomerState currentState;
        private Vector2 targetPosition;
        private bool hasTarget = false;
        private float patienceTimer = 0f;
        private SpriteRenderer spriteRenderer;

        public CustomerTypeData TypeData => typeData;
        public CustomerState CurrentState => currentState;

        public void SetTypeData(CustomerTypeData data)
        {
            typeData = data;
        }

        public void SetQueuePosition(Vector2 position)
        {
            targetPosition = position;
            hasTarget = true;
        }

        private void Start()
        {
            currentState = CustomerState.WaitingInQueue;
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Interact()
        {
            if (currentState != CustomerState.WaitingInQueue) return;

            currentState = CustomerState.Selected;
            QueueManager.Instance.SelectCustomer(this);
        }

        public bool CanInteract()
        {
            return currentState == CustomerState.WaitingInQueue;
        }

        public string GetInteractionPrompt()
        {
            return typeData != null ? $"Pilih {typeData.typeName}" : "Pilih pelanggan";
        }

        public void AssignToTable(Vector2 tablePosition)
        {
            targetPosition = tablePosition;
            hasTarget = true;
            currentState = CustomerState.WalkingToSeat;
        }

        private void Update()
        {
            HandleMovement();
            HandlePatience();
            UpdatePatienceColor();
        }

        private void HandleMovement()
        {
            if (!hasTarget) return;

            float distance = Vector2.Distance(transform.position, targetPosition);

            if (distance <= arrivalThreshold)
            {
                hasTarget = false;

                if (currentState == CustomerState.WalkingToSeat)
                {
                    currentState = CustomerState.Seated;
                    Debug.Log($"{typeData.typeName} sudah duduk.");
                }

                return;
            }

            transform.position = Vector2.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
        }

        private void HandlePatience()
        {
            // Kesabaran tidak berjalan kalau sedang makan, sudah selesai,
            // atau sudah pergi.
            if (currentState == CustomerState.Eating) return;
            if (currentState == CustomerState.Finished) return;
            if (currentState == CustomerState.Leaving) return;

            patienceTimer += Time.deltaTime;

            if (patienceTimer >= typeData.patienceDuration)
            {
                currentState = CustomerState.Leaving;
                Debug.Log($"{typeData.typeName} kehabisan kesabaran dan pergi!");

                QueueManager.Instance.RemoveFromQueue(this);
                // Untuk sekarang, langsung hilangkan dari scene.
                Destroy(gameObject, 1f);
            }
        }

        private void UpdatePatienceColor()
        {
            if (spriteRenderer == null) return;
            if (currentState == CustomerState.Leaving) return;

            float ratio = patienceTimer / typeData.patienceDuration;

            if (ratio < 0.5f)
            {
                // Hijau ke kuning (0% - 50%)
                spriteRenderer.color = Color.Lerp(Color.green, Color.yellow, ratio * 2f);
            }
            else
            {
                // Kuning ke merah (50% - 100%)
                spriteRenderer.color = Color.Lerp(Color.yellow, Color.red, (ratio - 0.5f) * 2f);
            }
        }
    }
}