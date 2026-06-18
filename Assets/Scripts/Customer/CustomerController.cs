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

        public CustomerTypeData TypeData => typeData;
        public CustomerState CurrentState => currentState;

        private void Start()
        {
            currentState = CustomerState.WaitingInQueue;

            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null && typeData != null)
            {
                sr.color = typeData.indicatorColor;
            }
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
            Debug.Log($"{typeData.typeName} berjalan menuju meja.");
        }

        private void Update()
        {
            if (!hasTarget) return;
            if (currentState != CustomerState.WalkingToSeat) return;

            float distance = Vector2.Distance(transform.position, targetPosition);

            if (distance <= arrivalThreshold)
            {
                hasTarget = false;
                currentState = CustomerState.Seated;
                Debug.Log($"{typeData.typeName} sudah duduk.");
                return;
            }

            transform.position = Vector2.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
        }
    }
}