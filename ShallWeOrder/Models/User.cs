using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShallWeOrder.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string UserId { get; set; }

        public string Password { get; set; }

        public int Gender { get; set; }

        [BsonDateTimeOptions(Kind = System.DateTimeKind.Local)]
        public DateTime Birthday { get; set; }

        [BsonDateTimeOptions(Kind = System.DateTimeKind.Local)]
        public DateTime Registerday { get; set; }

    }

}