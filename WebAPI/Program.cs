using KidsService;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

MySqlConnectionStringBuilder myBuilder = new MySqlConnectionStringBuilder()
{
    Database = "pkids",
    UserID = "admin",
    Password = "rootadmin",
    Server = "192.168.0.200",
    Port = 3306,
};

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<KidsContext>(options =>
{
    options.UseLazyLoadingProxies();
    options.UseMySQL(myBuilder.ConnectionString);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
