namespace TwoCS.TimeTracker.Data.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    [BsonIgnoreExtraElements]
    public class User : Domain.Models.User
    {
    }
}
