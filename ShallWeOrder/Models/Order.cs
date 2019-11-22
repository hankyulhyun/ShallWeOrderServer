
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShallWeOrder.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

    }
}