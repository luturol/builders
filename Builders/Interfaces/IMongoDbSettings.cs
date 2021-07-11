namespace Builders.Interfaces
{
    public interface IMongoDbSettings
    {
        string ConnectionString { get; set; }
        string Database { get; set; }
        string Collection { get; set; }

    }
}