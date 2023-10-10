using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ProjetS04_API.Models
{
    public class Vol
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? numeroVol { get; set; }

        public string villeDepart { get; set; } = null!;

        public string villeArrivee { get; set; } = null!;
        public string DateDepart { get; set; }

        public string DateArriver { get; set; }


        public string heureDepart { get; set; }

        public string heureArrivee { get; set; }

        public string idAvion { get; set; } = null!;
        public string nombrePlaces { get; set; } = null!;

    }
}
