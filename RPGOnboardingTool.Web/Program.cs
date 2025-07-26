using Microsoft.EntityFrameworkCore;
using RPGOnboardingTool.Core.Interfaces;
using RPGOnboardingTool.Application.Services;
using RPGOnboardingTool.Infrastructure.Data;
using RPGOnboardingTool.Infrastructure.Repositories;
using RPGOnboardingTool.Infrastructure.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICharacterService, CharacterService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.Converters.Add(new ByteArrayBase64Converter());
    });
builder.Services.AddRazorPages();

var app = builder.Build();

// Database initialization and seeding
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
    // For development: Only recreate database if it doesn't exist or if explicitly requested
    if (app.Environment.IsDevelopment())
    {
        // Check if we should force recreate the database (useful for schema changes)
        var forceRecreate = builder.Configuration.GetValue<bool>("Database:ForceRecreate", false);
        
        if (forceRecreate)
        {
            Console.WriteLine("🔄 Force recreating database due to configuration setting...");
            dbContext.Database.EnsureDeleted(); // Only delete if explicitly requested
            dbContext.Database.EnsureCreated(); // Recreate with latest seed data
            Console.WriteLine("✅ Database recreated with latest seed data");
        }
        else
        {
            // ensure the database exists without deleting existing data
            var created = dbContext.Database.EnsureCreated();
            if (created)
            {
                Console.WriteLine("✅ Database created with initial seed data");
            }
            else
            {
                Console.WriteLine("✅ Database already exists, preserving existing data");
            }
        }
    }
    else
    {
        dbContext.Database.Migrate(); // For production: use migrations
    }
}

// HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();

// Configure static files with cache headers for development
if (app.Environment.IsDevelopment())
{
    app.UseStaticFiles(new StaticFileOptions
    {
        OnPrepareResponse = ctx =>
        {
            // Disable caching for CSS and JS files in development
            if (ctx.File.Name.EndsWith(".css") || ctx.File.Name.EndsWith(".js"))
            {
                ctx.Context.Response.Headers.Append("Cache-Control", "no-cache, no-store, must-revalidate");
                ctx.Context.Response.Headers.Append("Pragma", "no-cache");
                ctx.Context.Response.Headers.Append("Expires", "0");
            }
        }
    });
}
else
{
    app.UseStaticFiles();
}

app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();

// Custom JSON converter for byte arrays
public class ByteArrayBase64Converter : System.Text.Json.Serialization.JsonConverter<byte[]>
{
    public override byte[] Read(ref System.Text.Json.Utf8JsonReader reader, Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
    {
        if (reader.TokenType == System.Text.Json.JsonTokenType.String)
        {
            var stringValue = reader.GetString();
            return stringValue != null ? Convert.FromBase64String(stringValue) : Array.Empty<byte>();
        }
        return reader.GetBytesFromBase64();
    }

    public override void Write(System.Text.Json.Utf8JsonWriter writer, byte[] value, System.Text.Json.JsonSerializerOptions options)
    {
        writer.WriteBase64StringValue(value);
    }
}