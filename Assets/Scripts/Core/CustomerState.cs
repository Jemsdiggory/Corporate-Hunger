namespace CorporateHunger.Core
{
    /// <summary>
    /// Merepresentasikan seluruh state yang bisa dilalui seorang pelanggan,
    /// dari masuk antrean sampai pergi. Player aktif memilih pelanggan
    /// dan mengarahkan ke meja, bukan drag-and-drop.
    /// </summary>
    public enum CustomerState
    {
        // Pelanggan menunggu di antrean, belum dipilih player.
        WaitingInQueue,

        // Player sudah klik pelanggan ini, menunggu player pilih meja.
        Selected,

        // Pelanggan sedang berjalan otomatis menuju meja yang ditentukan.
        WalkingToSeat,

        // Pelanggan sudah duduk, belum siap memesan.
        Seated,

        // Pelanggan siap memesan, menunggu pelayan datang catat pesanan.
        ReadyToOrder,

        // Pesanan sudah dicatat, pelanggan menunggu makanan datang.
        WaitingFood,

        // Makanan sudah diantar, pelanggan sedang makan.
        Eating,

        // Pelanggan selesai makan, siap meja dibersihkan.
        Finished,

        // Pelanggan meninggalkan restoran (selesai normal atau marah).
        Leaving
    }
}