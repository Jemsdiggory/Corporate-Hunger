namespace CorporateHunger.Core
{
    /// <summary>
    /// Kontrak untuk semua object yang bisa diinteraksi oleh player,
    /// misalnya meja, pelanggan, dan order counter.
    /// Player tidak perlu tahu jenis object yang diinteraksi,
    /// cukup panggil Interact() lewat interface ini.
    /// </summary>
    public interface IInteractable
    {
        // Dipanggil saat player menekan tombol interaksi (E)
        // dalam radius object ini.
        void Interact();

        // Teks singkat untuk ditampilkan di UI prompt,
        // contoh: "Antar Pelanggan", "Ambil Pesanan", "Bersihkan Meja".
        string GetInteractionPrompt();

        // Menentukan apakah object ini sedang bisa diinteraksi.
        // Contoh: meja kosong tidak perlu dibersihkan,
        // jadi CanInteract bernilai false.
        bool CanInteract();
    }
}