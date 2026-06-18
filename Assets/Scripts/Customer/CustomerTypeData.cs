using UnityEngine;

namespace CorporateHunger.Customer
{
    /// <summary>
    /// Data identitas satu tipe pelanggan (Anak Magang, Staff,
    /// Manager, Bos). Tidak ada logika di sini, cuma data,
    /// supaya balancing bisa diubah tanpa sentuh kode.
    /// </summary>
    [CreateAssetMenu(fileName = "CustomerType", menuName = "CorporateHunger/Customer Type")]
    public class CustomerTypeData : ScriptableObject
    {
        [Header("Identitas")]
        public string typeName;

        [Header("Kesabaran")]
        [Tooltip("Berapa detik sebelum pelanggan marah dan pergi")]
        public float patienceDuration = 30f;

        [Header("Reward")]
        [Tooltip("Gaji/tip yang didapat kalau pelanggan ini berhasil dilayani sampai selesai")]
        public int paymentValue = 10;

        [Header("Visual Placeholder")]
        [Tooltip("Warna kotak placeholder sebelum sprite asli ada")]
        public Color indicatorColor = Color.white;
    }
}