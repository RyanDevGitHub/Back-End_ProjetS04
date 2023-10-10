using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProjetS04_API.Models;

namespace ProjetS04_API.Services
{
    public class ReservationsService
    {
        private readonly IMongoCollection<Reservation> _reservationsCollection;

        public ReservationsService(
            IOptions<ProjetS04DatabaseSettings> projetS04DatabaseSettings)
        {
            var mongoClient = new MongoClient(
                projetS04DatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                projetS04DatabaseSettings.Value.DatabaseName);

            _reservationsCollection = mongoDatabase.GetCollection<Reservation>(
                projetS04DatabaseSettings.Value.ReservationsCollectionName);
        }

        public async Task<List<Reservation>> GetAsync() =>
            await _reservationsCollection.Find(_ => true).ToListAsync();

        public async Task<Reservation?> GetAsync(string id) =>
            await _reservationsCollection.Find(x => x.idReservation == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Reservation newReservation) =>
            //if newReservation.NumeroVol is in volColectionne
            await _reservationsCollection.InsertOneAsync(newReservation);

        public async Task UpdateAsync(string id, Reservation updatedReservation) =>
            await _reservationsCollection.ReplaceOneAsync(x => x.idReservation == id, updatedReservation);

        public async Task RemoveAsync(string id) =>
            await _reservationsCollection.DeleteOneAsync(x => x.idReservation == id);
    }
}
