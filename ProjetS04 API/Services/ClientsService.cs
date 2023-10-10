using ProjetS04_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;

namespace ProjetS04_API.Services
{
    public class ClientsService
    {
        private readonly IMongoCollection<Client> _clientsCollection;

        public ClientsService(
            IOptions<ProjetS04DatabaseSettings> projets04DatabaseSettings)
        {
            var mongoClient = new MongoClient(
                projets04DatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                projets04DatabaseSettings.Value.DatabaseName);

            _clientsCollection = mongoDatabase.GetCollection<Client>(
                projets04DatabaseSettings.Value.ClientsCollectionName);
        }

        public async Task<List<Client>> GetAsync() =>
            await _clientsCollection.Find(_ => true).ToListAsync();

        public async Task<Client?> GetAsync(string id) =>
            await _clientsCollection.Find(x => x.IdClient == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Client newClient)
        {
            var existingClient = await _clientsCollection.Find(client => client.passportNumber == newClient.passportNumber).FirstOrDefaultAsync();
            if (existingClient != null)
            {
                throw new ArgumentException("A client with the same passport number already exists in the database.");
            }
            await _clientsCollection.InsertOneAsync(newClient);
        }
        public async Task UpdateAsync(string id, Client updatedClient) =>
            await _clientsCollection.ReplaceOneAsync(x => x.IdClient == id, updatedClient);

        public async Task RemoveAsync(string id) =>
            await _clientsCollection.DeleteOneAsync(x => x.IdClient == id);
        public async Task<Client> Login(string firstname, string passportNumber)
        {
            var client = await _clientsCollection
                .Find(x => x.firstName == firstname && x.passportNumber == passportNumber)
                .FirstOrDefaultAsync();

            if (client != null)
            {
                // client trouvé, retourner une réponse HTTP 200 OK
                return client;
            }
            else
            {
                // client non trouvé, retourner une réponse HTTP 404 Not Found
                return null;
            }
        }

    }
}
