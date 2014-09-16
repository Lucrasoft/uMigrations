using System;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace Lucrasoft.Migrations.Database
{
    [TableName("Lucrasoft_Migrations")]
    [PrimaryKey("MigrationName", autoIncrement = false)]
    [ExplicitColumns]
    public sealed class DbMigration
    {
        [Column("MigrationName")]
        [PrimaryKeyColumn(AutoIncrement = false)]
        public string MigrationName { get; set; }

        [Column("MigrationDateTime")]
        public DateTime MigrationDateTime { get; set; }
    }
}
