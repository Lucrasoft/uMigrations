using System;
using Umbraco.Core;

namespace Lucrasoft.Migrations
{
    /// <summary>
    /// Default implementation of a migration.
    /// </summary>
    public class Migration : IMigration
    {
        /// <summary>
        /// The unique name for this migration. If the name already exists this migration will not run
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The action to execute as part of the migration
        /// </summary>
        public Action<ApplicationContext> MigrationAction { get; set; }
    }
}