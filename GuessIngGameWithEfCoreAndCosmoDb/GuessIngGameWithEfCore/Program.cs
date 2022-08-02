using GuessIngGameWithEfCoreAndCosmoDb.Repositories;
using GuessIngGameWithEfCoreAndCosmoDb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Cosmos;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
string account = builder.Configuration["CosmoDb:Account"];
string key = builder.Configuration["CosmoDb:Key"];
builder.Services.AddDbContext<GameDbContext>(options => options.UseCosmos(account, key));
builder.Services.AddScoped<IContestRepository, ContestRepository>();
builder.Services.AddScoped<IGuessRepository, GuessRepository>();
var app = builder.Build();
//using (var scope = app.Services.CreateScope())
//{
//    var gameDbContext = scope.ServiceProvider.GetRequiredService<GameDbContext>();
//    gameDbContext.Database.Migrate();
//}

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

