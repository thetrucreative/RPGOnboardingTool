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

//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    dbContext.Database.Migrate();
//}

// HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

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
            return Convert.FromBase64String(reader.GetString());
        }
        return reader.GetBytesFromBase64();
    }

    public override void Write(System.Text.Json.Utf8JsonWriter writer, byte[] value, System.Text.Json.JsonSerializerOptions options)
    {
        writer.WriteBase64StringValue(value);
    }
}