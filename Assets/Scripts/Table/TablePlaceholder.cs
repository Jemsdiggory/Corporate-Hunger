using UnityEngine;
using CorporateHunger.Core;

namespace CorporateHunger.Table
{
    public class TablePlaceholder : MonoBehaviour, IInteractable
    {
        [SerializeField] private string tableName = "Table";

        public void Interact()
        {
            Debug.Log($"{tableName}: berhasil diinteraksi oleh Player.");
        }

        public string GetInteractionPrompt()
        {
            return $"Pergi ke {tableName}";
        }

        public bool CanInteract()
        {
            return true;
        }
    }
}