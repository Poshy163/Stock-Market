#pragma warning disable CS0618
using System.IO;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using UnityEngine;
using BsonReader = Newtonsoft.Json.Bson.BsonReader;
using JsonConvert = Newtonsoft.Json.JsonConvert;

// ReSharper disable PossibleNullReferenceException
// ReSharper disable ParameterHidesMember
// ReSharper disable ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
public class MongoDBDatabase : MonoBehaviour
{
    private const string MongoLogin =
        "mongodb+srv://admin:admin@stockmarketapp.eal1kek.mongodb.net/?retryWrites=true&w=majority";

    private static readonly MongoClient Client = new MongoClient(MongoLogin);

    public static bool CommitRegister(string name, string userName, string passCode)
    {
        try
        {
            var filter = new BsonDocument {{"Name", name}};
            var database = Client.GetDatabase("UserDetails");
            var collection = database.GetCollection<BsonDocument>("Login Details");
            var documents = collection.Find(filter).ToList();
            JsonConvert.DeserializeObject(ToJson(documents[0]));
            return false;
        }
        catch
        {
            var database = Client.GetDatabase("UserDetails");
            var collection = database.GetCollection<BsonDocument>("Login Details");

            var document = new BsonDocument
            {
                {"Name", name},
                {"Username", userName},
                {"Passcode", passCode}
            };
            collection.InsertOne(document);
            return true;
        }
    }

    public static bool CheckLogin(string userName, string passCode)
    {
        try
        {
            var filter = new BsonDocument {{"Username", userName}};
            var database = Client.GetDatabase("UserDetails");
            var collection = database.GetCollection<BsonDocument>("Login Details");
            var documents = collection.Find(filter).ToList();
            dynamic jsonFile = JsonConvert.DeserializeObject(ToJson(documents[0]));
            return jsonFile["Passcode"] == passCode;
        }
        catch
        {
            return false;
        }
    }

    private static string ToJson(BsonDocument bson)
    {
        var stream = new MemoryStream();
        using (var writer = new BsonBinaryWriter(stream))
        {
            BsonSerializer.Serialize(writer, typeof(BsonDocument), bson);
        }

        stream.Seek(0, SeekOrigin.Begin);
        var reader = new BsonReader(stream);
        var sb = new StringBuilder();
        var sw = new StringWriter(sb);
        using (var jWriter = new JsonTextWriter(sw))
        {
            jWriter.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            jWriter.WriteToken(reader);
        }

        return sb.ToString();
    }
}