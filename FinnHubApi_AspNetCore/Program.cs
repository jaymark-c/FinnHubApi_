using FinnHubApi_AspNetCore.Services;

var builder = WebApplication.CreateBuilder(args);

//Services
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddTransient<IFinnhubService, Finnhub>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
