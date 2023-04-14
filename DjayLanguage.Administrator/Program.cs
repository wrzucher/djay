using AutoMapper;
using DjayLanguage.Core;
using DjayLanguage.Core.EntityFramework;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSingleton<Profile, EntityFrameworkMappingProfile>();

builder.Services.AddSingleton<AutoMapper.IConfigurationProvider>(serviceProvider =>
{
    var profiles = serviceProvider.GetRequiredService<IEnumerable<Profile>>();
    var mappingConfig = new MapperConfiguration(mc =>
    {
        foreach (var profile in profiles)
        {
            mc.AddProfile(profile);
        }
    });
    return mappingConfig;
});

builder.Services.AddScoped<IMapper, Mapper>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<DjayDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<WordManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
