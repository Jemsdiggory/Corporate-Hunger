namespace CorporateHunger.Core
{
    /// <summary>
    /// Merepresentasikan seluruh state yang bisa dilalui seorang pelanggan
    /// dari masuk restoran sampai pergi.
    /// Dipakai oleh CustomerController dan UI untuk menentukan
    /// tampilan serta perilaku pelanggan di setiap fase.
    /// </summary>
    public enum CustomerState
    {
        // Pelanggan baru masuk, menunggu diantar ke meja oleh pelayan.
        WaitingEntrance,

        // Pelanggan sedang berjalan menuju meja yang ditentukan.
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

        // Pelanggan meninggalkan restoran.
        Leaving
    }
}