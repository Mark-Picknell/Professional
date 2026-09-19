# DatabaseXmlManager

Home for `DatabaseXmlManager.sln`, a Windows desktop application built with C# and WPF for reading and writing XML files and working with Microsoft SQL Server records.

## Planned capabilities

- Read, validate, and write XML files.
- Insert, update, and delete SQL Server records.
- Select among DEV, IST, UAT, BCP, and PROD database environments.
- Keep database access, XML processing, and WPF presentation separated.

## Dependencies

Use Microsoft libraries and APIs only unless third-party dependencies are explicitly approved.

## Solution structure

- `DatabaseXmlManager.App` — WPF shell, MVVM view models, and dependency-injection composition root.
- `DatabaseXmlManager.Core` — domain models, contracts, environment state, and application-independent logic.
- `DatabaseXmlManager.SqlServer` — SQL Server connection factory and repository implementations.
- `DatabaseXmlManager.Xml` — XML reading and writing through LINQ to XML.
- `DatabaseXmlManager.Tests` — MSTest coverage for the core and XML layers.

Open `DatabaseXmlManager.sln` with Visual Studio 2026 and replace the placeholder server and database names in `src/DatabaseXmlManager.App/appsettings.json`. PROD and BCP writes default to disabled.
