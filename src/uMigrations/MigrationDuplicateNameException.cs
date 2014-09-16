using System;

namespace Lucrasoft.Migrations
{
    /// <summary>
    /// Exception for when a migration exists with a duplicate name
    /// </summary>
    public sealed class MigrationDuplicateNameException : Exception
    {
        public MigrationDuplicateNameException(string name)
            : base(string.Format("There already is a migration with the name: {0}", name))
        {
        }
    }
}