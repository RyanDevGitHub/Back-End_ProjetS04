using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ProjetS04_API.Models
{
    public class Reservation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? idReservation { get; set; }

        public string numeroVol { get; set; } = null!;

        public string idClient { get; set; } = null!;
    }
}
