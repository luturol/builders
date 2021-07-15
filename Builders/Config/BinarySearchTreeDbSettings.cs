using Builders.Interfaces;

namespace Builders.Config
{
    public class BinarySearchTreeDbSettings : IMongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public string Collection { get; set; }
    }
}