using Microsoft.EntityFrameworkCore;
using RaceDayApi.Data;
using RaceDayApi.Services;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new() { Title = "RaceDay API", Version = "v1" });
});
builder.Services.AddSingleton<BlobService>();

builder.Services.AddDbContext<RaceDayContext>(opt => {
    var conn = builder.Configuration.GetConnectionString("DefaultConnection");
    opt.UseInMemoryDatabase("RaceDayDb");
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(o => {
    o.IdleTimeout = TimeSpan.FromHours(2);
    o.Cookie.HttpOnly = true;
    o.Cookie.IsEssential = true;
});

var app = builder.Build();
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI();
app.UseSession();
app.UseAuthorization();
app.MapControllers();
app.Run();
public partial class Program { }