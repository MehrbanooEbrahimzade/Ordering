using Npgsql;

namespace Accounting.Api.Extensions
{
    public static class HostExtension
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();

                try
                {
                    logger.LogInformation("Migrating postresql database.");

                    using var connection = new NpgsqlConnection
                        (configuration.GetConnectionString("AccountingDb"));
                    connection.Open();

                    using var command = new NpgsqlCommand
                    {
                        Connection = connection
                    };

                    command.CommandText = "DROP TABLE IF EXISTS Accounting";
                    command.ExecuteNonQuery();

                    command.CommandText = "CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\" ";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE Accounting(Id uuid NOT NULL,
                                             ProductId INT NOT NULL,
                                             Amount INT NOT NULL,
                                             UserId uuid NOT NULL,
                                             UserName VARCHAR(50) NOT NULL,
                                             Number VARCHAR(50) NOT NULL,
                                             EmailAddress VARCHAR(320) NOT NULL,
                                             CreatedDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                                             PRIMARY KEY(Id))";
                    command.ExecuteNonQuery();

                    logger.LogInformation("Migrated postresql database.");


                    //command.CommandText =
                    //    @"INSERT INTO Accounting(Id, ProductId,Amount, UserId,UserName,Number,EmailAddress,CreatedDate)
                    //                      VALUES('3812eadf-86ce-464b-a5a2-be2f57a83fec',23,2,'3812eadf-86ce-464b-a5a2-be2f57a83fec','mehri',
                    //                        '0911', 'mehri@gmail.com', '2016-06-22 19:10:25-07')";
                    //command.ExecuteNonQuery();

                }
                catch (NpgsqlException ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the postresql database");

                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host);
                    }
                }
            }
            return host;
        }
    }
}
