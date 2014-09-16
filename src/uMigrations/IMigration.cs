using System;
using Umbraco.Core;

namespace Lucrasoft.Migrations
{
    /// <summary>
    /// Defines a migration for uMigrations
    /// </summary>
    public interface IMigration
    {
        /// <summary>
        /// The unique name for this migration. If the name already exists this migration will not run
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The action to execute as part of the migration
        /// </summary>
        Action<ApplicationContext> MigrationAction { get; set; }
    }
}