namespace TsqltNet
{
    public interface IDbMigrator
    {
        void Migrate(string connectionString);
    }
}
