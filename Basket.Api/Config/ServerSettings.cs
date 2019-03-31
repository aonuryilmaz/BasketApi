namespace Basket.Api.Config
{
    public class ServerSettings
    {
        public MongoOptions MongoDB { get; set; }= new MongoOptions();
    }

    public class MongoOptions
    {
        public string Database { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }

        public string ConnectionString => $@"mongodb://{Host}:{Port}";
    }
}