using System.Collections.Generic;

namespace Lucrasoft.Migrations
{
    /// <summary>
    /// Provides migrations for the <see cref="Migrator"/> class. There can be multiple <see cref="IMigrationsProvider"/> classes for a single project
    /// </summary>
    public interface IMigrationsProvider
    {
        /// <summary>
        /// Gets all the migrations from this class
        /// </summary>
        IEnumerable<IMigration> GetMigrations();
    }
}