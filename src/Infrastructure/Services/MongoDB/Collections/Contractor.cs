using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Infrastructure.MongoDB.Collections;

public class Contractor
{
    [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
    public ObjectId? Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Name { get; set; }

    public string City { get; set; }

    public string Street { get; set; }

    public string NIP { get; set; }

    public string ZipCode { get; set; }

    public string Email { get; set; }
}
