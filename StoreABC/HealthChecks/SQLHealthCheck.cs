using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using StoreABC.Options;
using System.Data.SqlClient;

namespace StoreABC.HealthChecks
{
    public class SQLHealthCheck : IHealthCheck
    {
        private readonly string _connString;
        public SQLHealthCheck(IOptions<CustomOption> customOption)
        {
            _connString = customOption.Value.ConnectionString;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                using var sqlConnection = new SqlConnection(_connString);

                await sqlConnection.OpenAsync(cancellationToken);

                using var command = sqlConnection.CreateCommand();
                command.CommandText = "SELECT 1";

                await command.ExecuteScalarAsync(cancellationToken);

                return HealthCheckResult.Healthy();
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(context.Registration.FailureStatus.ToString(), exception: ex);
            }
        }
    }
}
