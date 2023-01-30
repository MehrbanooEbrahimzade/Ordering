﻿using Npgsql;

namespace Email.Api.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static void MigrateDatabase<TContext>(this IServiceProvider sp, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            using (var scope = sp.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();

                try
                {
                    logger.LogInformation("Migrating postresql database.");

                    using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                    connection.Open();

                    using var command = new NpgsqlCommand
                    {
                        Connection = connection
                    };

                    command.CommandText = "DROP TABLE IF EXISTS Email; CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\" ";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE Email(Id uuid DEFAULT uuid_generate_v4 (),
                                                           OrderId uuid NOT NULL,
                                                           UserName VARCHAR(50) NOT NULL,
                                                           EmailAddress VARCHAR(320) NOT NULL,
                                                           Amount INT NOT NULL,
                                                           CreatedDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                                                           PRIMARY KEY (Id))";
                    command.ExecuteNonQuery();
                    logger.LogInformation("Migrated postresql database.");
                }
                catch (NpgsqlException ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the postresql database");

                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(sp, retryForAvailability);
                    }
                }
            }

        }
        
    }
}
