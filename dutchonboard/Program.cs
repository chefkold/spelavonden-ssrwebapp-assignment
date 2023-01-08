using dutchonboard.Core.DomainServices.Managers;
using dutchonboard.Core.DomainServices.Repositories;
using dutchonboard.Infrastructure.EF.Data;
using dutchonboard.Infrastructure.EF.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DutchOnBoardDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("dutchonboarddb")));

builder.Services.AddDbContext<DutchOnBoardSecurityDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("dutchonboarddbsecurity")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<DutchOnBoardSecurityDbContext>().AddDefaultTokenProviders();

builder.Services.AddAuthorization(options => options.AddPolicy("GameNightOrganizer", policy => policy.RequireClaim("Organizer")));
builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/Auth";
});


builder.Services.AddTransient<DataSeeder>();
builder.Services.AddTransient<DataSeederSecurity>();

builder.Services.AddScoped<IGameNightManager, GameNightManager>(); 

builder.Services.AddScoped<IGameNightRepo, GameNightRepo>();
builder.Services.AddScoped<IBoardGameRepo, BoardGameRepo>();
builder.Services.AddScoped<IOrganizerRepo, OrganizerRepo>();
builder.Services.AddScoped<IPlayerRepo, PlayerRepo>();

builder.Services.AddControllersWithViews();

var app = builder.Build();
await SeedData(app);

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Login",
    pattern: "Auth",
    defaults: new { controller = "Auth", action = "Login" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


static async Task SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using var scope = scopedFactory!.CreateScope();
    var serviceMainSeeder = scope.ServiceProvider.GetService<DataSeeder>();
    var serviceSecuritySeeder = scope.ServiceProvider.GetService<DataSeederSecurity>();

    await serviceMainSeeder!.Seed();
    serviceSecuritySeeder!.Seed().Wait();
}