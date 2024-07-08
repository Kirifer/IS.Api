using System.Diagnostics;

using Is.Core.Database.Abstraction.Interface;
using Is.Core.Database.Migration;

using Microsoft.Extensions.Logging;

namespace Is.DataLayer
{
    public class AtsDbMigration : DbUpMigrationBase, IDbMigration
    {
        private readonly ILogger<AtsDbMigration> _logger;
        private readonly IDbConfig _dbSettings;

        public AtsDbMigration(
            ILogger<AtsDbMigration> logger,
            IDbConfig dbSettings)
            : base(logger)
        {
            _logger = logger;
            _dbSettings = dbSettings;
        }

        public void ExecuteMigration()
        {
            try
            {
                var connectionString = _dbSettings.ConnectionString;

                // Perform creation of database if not exist
                CreateDatabaseIfNotExist(connectionString);
                PerformScriptExecution(connectionString);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                _logger.LogError($"Migration failed: {ex.Message}");
            }
        }
    }
}
