namespace ProjetS04_API.Models
{
    public class ProjetS04DatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string ClientsCollectionName { get; set; } = null!;
        public string VolsCollectionName { get; set; } = null!;

        public string ReservationsCollectionName { get; set; } = null!;
    }
}
