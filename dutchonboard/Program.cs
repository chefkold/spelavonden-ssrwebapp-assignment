using dutchonboard.Core.DomainServices.Repositories;
using dutchonboard.Infrastructure.EF.Data;
using dutchonboard.Infrastructure.EF.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DutchOnBoardDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("dutchonboarddb")));
builder.Services.AddTransient<DataSeeder>();
builder.Services.AddScoped<IGameNightRepo, GameNightRepo>();

builder.Services.AddControllersWithViews();

var app = builder.Build();
SeedData(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


static void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using var scope = scopedFactory!.CreateScope();
    var service = scope.ServiceProvider.GetService<DataSeeder>();
    service!.Seed();
}