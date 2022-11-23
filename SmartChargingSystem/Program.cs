using SCS.BLL;
using SCS.DAL;
using SCS.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<SmartChargingContext, SmartChargingContext>();
builder.Services.AddDbContext<SmartChargingContext>();

builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IConnectorRepository, ConnectorRepository>();
builder.Services.AddScoped<IChargeStationRepository, ChargeStationRepository>();

builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IConnectorService, ConnectorService>();
builder.Services.AddScoped<IChargeStationService, ChargeStationService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
