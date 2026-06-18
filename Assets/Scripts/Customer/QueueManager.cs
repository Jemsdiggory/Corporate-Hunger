using System.Collections.Generic;
using UnityEngine;

namespace CorporateHunger.Customer
{
    public class QueueManager : MonoBehaviour
    {
        public static QueueManager Instance { get; private set; }

        [Header("Queue Settings")]
        [SerializeField] private Transform queueStartPoint;
        [SerializeField] private float spacingX = 1.2f;

        private List<CustomerController> queue = new List<CustomerController>();
        private CustomerController selectedCustomer;

        private void Awake()
        {
            Instance = this;
        }

        public void AddToQueue(CustomerController customer)
        {
            queue.Add(customer);
            UpdateQueuePositions();
        }

        public void SelectCustomer(CustomerController customer)
        {
            selectedCustomer = customer;
        }

        public CustomerController GetSelectedCustomer()
        {
            return selectedCustomer;
        }

        public void ClearSelectedCustomer()
        {
            selectedCustomer = null;
        }

        public void RemoveFromQueue(CustomerController customer)
        {
            queue.Remove(customer);

            if (selectedCustomer == customer)
            {
                selectedCustomer = null;
            }

            UpdateQueuePositions();
        }

        private void UpdateQueuePositions()
        {
            for (int i = 0; i < queue.Count; i++)
            {
                Vector2 queuePosition = (Vector2)queueStartPoint.position
                    + new Vector2(i * spacingX, 0f);

                queue[i].SetQueuePosition(queuePosition);
            }
        }
    }
}