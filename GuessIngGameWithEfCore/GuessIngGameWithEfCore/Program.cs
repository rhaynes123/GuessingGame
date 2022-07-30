using GuessIngGameWithEfCore.Repositories;
using GuessIngGameWithEfCore.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
string connection = builder.Configuration.GetConnectionString("GameConnection");
builder.Services.AddDbContext<GameDbContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));
builder.Services.AddScoped<IContestRepository, ContestRepository>();
builder.Services.AddScoped<IGuessRepository, GuessRepository>();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var gameDbContext = scope.ServiceProvider.GetRequiredService<GameDbContext>();
    gameDbContext.Database.Migrate();
}

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

