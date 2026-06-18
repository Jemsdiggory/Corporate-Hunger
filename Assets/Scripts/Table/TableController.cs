using UnityEngine;
using CorporateHunger.Core;
using CorporateHunger.Customer;

namespace CorporateHunger.Table
{
    /// <summary>
    /// Mengatur satu meja: status kosong/terisi, dan menerima
    /// pelanggan yang sedang dipilih dari QueueManager begitu
    /// player klik meja ini.
    /// </summary>
    public class TableController : MonoBehaviour, IInteractable
    {
        private enum TableState
        {
            Empty,
            Occupied,
            NeedsCleaning
        }

        [SerializeField] private string tableName = "Table";
        private TableState currentState = TableState.Empty;

        public void Interact()
        {
            if (currentState == TableState.Empty)
            {
                CustomerController selected = QueueManager.Instance.GetSelectedCustomer();

                if (selected == null)
                {
                    Debug.Log($"{tableName}: tidak ada pelanggan yang dipilih."); //kalo gaada pelanggan yang dipilih, kasih tau player lewat debug log
                    return;
                }

                Debug.Log($"{tableName}: menerima {selected.TypeData.typeName}."); //table berapa, nerima pelanggan tipe apa
                QueueManager.Instance.ClearSelectedCustomer(); 
                currentState = TableState.Occupied; 

                selected.AssignToTable(transform.position); //kasi tau pelanggan yang dipilih untuk jalan ke meja ini
            }
            else if (currentState == TableState.NeedsCleaning)  //kalo meja butuh dibersihin, interaksi ini akan bersihin meja dan balikkin ke status kosong
            {
                Debug.Log($"{tableName}: dibersihkan, kembali kosong.");  //kasih tau lewat debug log kalau meja udah dibersihin
                currentState = TableState.Empty;
            }
            else
            {
                Debug.Log($"{tableName}: sedang terisi.");  //kasih tau player lewat debug log, kalo meja ini terisi 
            }
        }

        public bool CanInteract() => true;

        public string GetInteractionPrompt()
        {
            return currentState switch
            {
                TableState.Empty => $"Antar pelanggan ke {tableName}",
                TableState.NeedsCleaning => $"Bersihkan {tableName}",
                _ => $"{tableName} terisi"
            };
        }
    }
}