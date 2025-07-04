var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); 
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();           

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection(); 
app.UseStaticFiles();      
app.UseRouting();          
app.UseAuthorization();    
app.MapControllers();      
app.MapFallbackToFile("index.html");
app.Run();