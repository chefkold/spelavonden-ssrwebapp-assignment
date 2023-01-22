using dutchonboard.Core.DomainServices.Repositories;
using dutchonboard.Infrastructure.EF.Data;
using dutchonboard.Infrastructure.EF.Repositories;
using dutchonboardGraphQLApi.GraphQl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DutchOnBoardDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("dutchonboarddb")));
builder.Services.AddScoped<IGameNightRepo, GameNightRepo>();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<GameNightQuery>()
    .AddType<GameNightQuery>();

var app = builder.Build();
app.MapGraphQL();
app.UseRouting();
app.Run();