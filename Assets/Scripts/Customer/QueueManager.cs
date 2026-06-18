using UnityEngine;

namespace CorporateHunger.Customer
{
    /// <summary>
    /// Satu titik koordinasi untuk menyimpan pelanggan yang sedang
    /// dipilih player. Table akan tanya ke sini untuk tahu siapa
    /// yang harus diarahkan begitu meja diklik.
    /// </summary>
    public class QueueManager : MonoBehaviour
    {
        public static QueueManager Instance { get; private set; }

        private CustomerController selectedCustomer;

        private void Awake()
        {
            Instance = this;
        }

        public void SelectCustomer(CustomerController customer)
        {
            selectedCustomer = customer;
            Debug.Log($"QueueManager: {customer.TypeData.typeName} sekarang menjadi selectedCustomer.");
        }

        public CustomerController GetSelectedCustomer()
        {
            return selectedCustomer;
        }

        public void ClearSelectedCustomer()
        {
            selectedCustomer = null;
        }
    }
}