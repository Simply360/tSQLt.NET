namespace Tsqlt
{
    public interface IDbMigrator
    {
        void Migrate(string connectionString);
    }
}
