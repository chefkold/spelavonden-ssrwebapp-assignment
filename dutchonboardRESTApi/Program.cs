using dutchonboard.Core.DomainServices.Repositories;
using dutchonboard.Core.DomainServices.Services;
using dutchonboard.Infrastructure.EF.Data;
using dutchonboard.Infrastructure.EF.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DutchOnBoardDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("dutchonboarddb")));
builder.Services.AddDbContext<DutchOnBoardSecurityDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("dutchonboarddbsecurity")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<DutchOnBoardSecurityDbContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<IGameNightService, GameNightService>();
builder.Services.AddScoped<IGameNightRepo, GameNightRepo>();
builder.Services.AddScoped<IBoardGameRepo, BoardGameRepo>();
builder.Services.AddScoped<IOrganizerRepo, OrganizerRepo>();
builder.Services.AddScoped<IPlayerRepo, PlayerRepo>();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.Run();