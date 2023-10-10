using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProjetS04_API.Models;

namespace ProjetS04_API.Services
{
    public class VolsService
    {
        private readonly IMongoCollection<Vol> _volsCollection;

        public VolsService(
            IOptions<ProjetS04DatabaseSettings> projetS04DatabaseSettings)
        {
            var mongoClient = new MongoClient(
                projetS04DatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                projetS04DatabaseSettings.Value.DatabaseName);

            _volsCollection = mongoDatabase.GetCollection<Vol>(
                projetS04DatabaseSettings.Value.VolsCollectionName);
        }

        public async Task<List<Vol>> GetAsync() =>
            await _volsCollection.Find(_ => true).ToListAsync();

        public async Task<Vol?> GetAsync(string id) =>
            await _volsCollection.Find(x => x.numeroVol == id).FirstOrDefaultAsync();

        public async Task<Vol?> GetAsyncByParam(string villeDepart, string villeArriver, string dateDepart, string dateArriver, string heure_depart, string heure_arrivee) {
            var vol = await _volsCollection.Find(x => x.villeDepart == villeDepart && x.villeArrivee == villeArriver && x.DateArriver == dateArriver && x.heureDepart == heure_depart && x.heureArrivee == heure_arrivee).FirstOrDefaultAsync();
            if (vol != null)
            {
                // client trouvé, retourner une réponse HTTP 200 OK
                return vol;
            }
            else
            {
                // client non trouvé, retourner une réponse HTTP 404 Not Found
                return null;
            }
        }

        public async Task<Vol?> CreateAsync(Vol newVol)
        {
            var vol = await _volsCollection.Find(x => x.idAvion == newVol.idAvion).FirstOrDefaultAsync();
            if (vol != null)
            {
                // client trouvé, retourner une réponse HTTP 200 OK
                return null;
            }
      
            await _volsCollection.InsertOneAsync(newVol);
            return newVol;
        }

        public async Task UpdateAsync(string id, Vol updatedVol) =>
            await _volsCollection.ReplaceOneAsync(x => x.numeroVol == id, updatedVol);

        public async Task RemoveAsync(string id) =>
            await _volsCollection.DeleteOneAsync(x => x.numeroVol == id);
    }
}

