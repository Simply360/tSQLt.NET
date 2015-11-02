using System.Data.SqlClient;
using System.Reflection;

namespace TsqltNet
{
    public class EmbeddedResourceTsqltInstaller : ITsqltInstaller
    {
        private readonly IEmbeddedTextResourceReader _embeddedTextResourceReader;
        private readonly ISqlBatchExtractor _sqlBatchExtractor;

        protected virtual string TsqltInstallationResourcePath => "TsqltNet.tSQLt.class.sql";

        public EmbeddedResourceTsqltInstaller(IEmbeddedTextResourceReader embeddedTextResourceReader, ISqlBatchExtractor sqlBatchExtractor)
        {
            _embeddedTextResourceReader = embeddedTextResourceReader;
            _sqlBatchExtractor = sqlBatchExtractor;
        }

        public void Install(string connectionString)
        {
            var tsqltInstallationContents = _embeddedTextResourceReader.GetResourceContents(Assembly.GetExecutingAssembly(), TsqltInstallationResourcePath);
            var batches = _sqlBatchExtractor.ExtractBatches(tsqltInstallationContents);

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var batch in batches)
                {
                    using (var cmd = new SqlCommand(batch, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}