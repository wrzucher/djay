# Djay Language - best project for learning language- [I hope]

## Database migration

We use code-first because we want to use PostgreSQL.
Create a new migration (Do not forget to change the migration name):

dotnet ef migrations add Initial --project ../DjayLanguage.Core.EntityFramework --output-dir ../DjayLanguage.Core.EntityFramework --namespace DjayLanguage.Core.EntityFramework --context DjayDbContext