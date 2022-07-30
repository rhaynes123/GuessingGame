using GuessingGameWithCosmodb.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
var loggerFactory =
builder.Services.AddSingleton<IContestRepository, ContestRepository>(options => {
    string url = builder.Configuration["CosmoDb:Account"];
    string key = builder.Configuration["CosmoDb:Key"];
    CosmosClient cosmosClient = new (url,key);
    return new ContestRepository(cosmosClient,"GameDb","GameContainer");
});
builder.Services.AddScoped<IGuessRepository, GuessRepository>(options => {
    string url = builder.Configuration["CosmoDb:Account"];
    string key = builder.Configuration["CosmoDb:Key"];
    CosmosClient cosmosClient = new(url, key);
    return new GuessRepository(cosmosClient, "GameDb", "GameContainer");
});
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

