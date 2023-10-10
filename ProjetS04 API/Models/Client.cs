using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace ProjetS04_API.Models
{
    public class Client
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? IdClient { get; set; }

        public string firstName { get; set; } = null!;

        public string lastName { get; set; } = null!;

        public string address { get; set; } = null!;
        public string email { get; set; } = null!;

        public string tel { get; set; } = null!;
        public DateTime  birthday { get; set; }
        public string passportNumber { get; set; } = null!;

    }
}
