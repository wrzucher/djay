namespace DjayLanguage.Core.EntityFramework;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

public class DjayDbContext : DbContext
{
    public DjayDbContext(DbContextOptions options)
    : base(options)
    {
    }

    public DbSet<Word> Words { get; set; }
    public DbSet<WordDefinition> WordDefinitions { get; set; }
    public DbSet<WordExample> WordExamples { get; set; }
}

