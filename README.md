# Djay Language - best project for learning language- [I hope]

## Database migration

We use code-first because we want to use PostgreSQL but at this moment we use SQL Server only.
It looks like migrations depend on it and we need to re-create migration for PostgreSQL in future.

Create a new migration (Do not forget to change the migration name):

dotnet ef migrations add InitialWordsOnly --project ../DjayLanguage.Core.EntityFramework --output-dir ../DjayLanguage.Core.EntityFramework/Migrations --context DjayDbContext

Do not forget update databases before you start:

dotnet ef database update --context ApplicationDbContext
dotnet ef database update --context DjayDbContext