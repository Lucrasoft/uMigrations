uMigrations
===========

Adds migration capabilities to your Umbraco installation (v7 and up) for smoother deployments

What is uMigrations?
-----------

uMigrations enables the developer to make changes to the Umbraco database from code using the available API's from Umbraco. Each migration will run only once and in the exact order you've entered them. On startup uMigrations will check the version of the database and determine which migrations need to run in order for the application to be up to date.

What is uMigrations NOT
-----------

uMigrations is not a Code-First library, nor does it aspires to be one. uMigrations will help the developers to programmatically change the Umbraco database (Pages, settings, document types, templates etc) without hitting the backend. This way changes are stored into source control and are replicable across multiple databases (for example: multiple developer's databases, a test environment and (one or more) prodution environments)

Usage
-----------

See https://github.com/Lucrasoft/uMigrations/wiki/Usage for exmaples
