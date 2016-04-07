using System;
using System.Collections.Generic;
using System.Linq;
using Lucrasoft.Migrations.Database;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;

namespace Lucrasoft.Migrations
{
    /// <summary>
    /// Entry point to run migrations. Use this class to run migration classes
    /// </summary>
    public class Migrator
    {
        private readonly IEnumerable<IMigrationsProvider> migrationProviders;
        private static readonly string Version = typeof (Migrator).Assembly.GetName().Version.ToString();

        public Migrator(IEnumerable<IMigrationsProvider> migrationProviders)
        {
            this.migrationProviders = migrationProviders;
        }

        public Migrator(IMigrationsProvider migrationsProvider)
        {
            this.migrationProviders = new [] {migrationsProvider};
        }

        /// <summary>
        /// Runs all the migrations given in the constructor in the correct sequence
        /// </summary>
        /// <param name="applicationContext">The Umbraco ApplicationConext instance to access using the migrations</param>
        /// <exception cref="MigrationDuplicateNameException">Thrown when a duplicate migration name exists</exception>
        public void RunMigrations(ApplicationContext applicationContext)
        {
            if (!applicationContext.IsConfigured || !applicationContext.DatabaseContext.IsDatabaseConfigured)
            {
                LogHelper.Debug<Migrator>(string.Format("Lucrasoft uMigrations (version {0}) - No need to run migrations now, the application is not configured or the database is not configured", Version));
                return;
            }

            CreateDatabaseTable(applicationContext);

            var migrationsDictionary = ExtractMigrations();

            LogHelper.Debug<Migrator>(string.Format("Lucrasoft uMigrations (version {0}) - Found the following migrations: {1}",
                                      Version, string.Join(",", migrationsDictionary.Keys)));

            LogHelper.Info<Migrator>(string.Format("Lucrasoft uMigrations (version {0}) - Running migrations", Version));
            ExecuteMigrations(applicationContext, migrationsDictionary);
        }

        private Dictionary<string, IMigration> ExtractMigrations()
        {
            var migrationsDictionary = new Dictionary<string, IMigration>();

            foreach (var migrationProvider in migrationProviders)
            {
                foreach (var migration in migrationProvider.GetMigrations())
                {
                    if (migrationsDictionary.ContainsKey(migration.Name))
                    {
                        throw new MigrationDuplicateNameException(migration.Name);
                    }

                    migrationsDictionary.Add(migration.Name, migration);
                }
            }
            return migrationsDictionary;
        }

        private static void CreateDatabaseTable(ApplicationContext applicationContext)
        {
            var db = applicationContext.DatabaseContext.Database;

            if (!db.TableExist("Lucrasoft_Migrations"))
            {
                db.CreateTable<DbMigration>(false);
            }
        }

        private static void ExecuteMigrations(ApplicationContext applicationContext, Dictionary<string, IMigration> migrations)
        {
            var db = applicationContext.DatabaseContext.Database;
            var databaseMigrations = db.Query<DbMigration>("SELECT * FROM Lucrasoft_Migrations")
                                       .Select(x => x.MigrationName)
                                       .ToList();

            foreach (var migration in migrations)
            {
                if (databaseMigrations.Contains(migration.Key, CaseInsensitiveStringComparer.Instance))
                {
                    LogHelper.Debug<Migrator>(string.Format("Lucrasoft uMigrations (version {0}) - Skipping migration '{1}' because it has already been executed", Version, migration.Key));
                    continue;
                }

                try
                {
                    LogHelper.Info<Migrator>(string.Format("Lucrasoft uMigrations (version {0}) - Running migration with key '{1}'", Version, migration.Key));
                    migration.Value.MigrationAction.Invoke(applicationContext);
                }
                catch (Exception ex)
                {
                    LogHelper.Error<Migrator>(string.Format("Lucrasoft uMigrations (version {0}) - Migration '{1}' failed: {2}", Version, migration.Key, ex.Message), ex);
                    throw;
                }

                db.Insert(new DbMigration
                {
                    MigrationDateTime = DateTime.Now,
                    MigrationName = migration.Key
                });

                databaseMigrations.Add(migration.Key);
            }
        }
    }
}
