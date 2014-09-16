using System;
using System.Collections.Generic;
using Umbraco.Core;

namespace Lucrasoft.Migrations
{
    /// <summary>
    /// Abstract implmentation of the <see cref="IMigrationsProvider"/> with helper methods to easily add migrations
    /// </summary>
    public abstract class AbstractMigrationsProvider : IMigrationsProvider
    {
        private readonly List<IMigration> migrations = new List<IMigration>(); 
        
        /// <summary>
        /// Adds a migration to the collection using a name and an action to represent the migration
        /// </summary>
        /// <param name="name">The unique name of the migration</param>
        /// <param name="migration">The actual migration code</param>
        protected void AddMigration(string name, Action<ApplicationContext> migration)
        {
            migrations.Add(new Migration
            {
                Name = name,
                MigrationAction = migration
            });
        }

        /// <summary>
        /// Adds a migration to the collection using a direct class
        /// </summary>
        /// <param name="migration">The actual migration code</param>
        protected void AddMigration(IMigration migration)
        {
            migrations.Add(migration);
        }

        /// <summary>
        /// Returns all the migrations in the collection
        /// </summary>
        public IEnumerable<IMigration> GetMigrations()
        {
            return migrations;
        }
    }
}
