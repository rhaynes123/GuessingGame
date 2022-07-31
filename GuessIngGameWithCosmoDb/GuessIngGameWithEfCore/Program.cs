using GuessingGameWithCosmodb.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IContestRepository, ContestRepository>(options => {
    string url = builder.Configuration["CosmoDb:Account"];
    string key = builder.Configuration["CosmoDb:Key"];
    CosmosClient cosmosClient = new (url,key);
    return new ContestRepository(cosmosClient,"GameDb","GameContainer");//TODO move these values into a config I was lazy but this is gross
});
//TODO consider removing this altogether. The data is in a document format and isn't relational.
//Also the partion key is based off contest number it might not be worth while to have a repo for data that isn't partioned differently.
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

