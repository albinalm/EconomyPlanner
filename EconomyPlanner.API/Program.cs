using EconomyPlanner.API;
using EconomyPlanner.API.Interfaces;
using EconomyPlanner.API.Services;
using EconomyPlanner.Repository.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowBlazorOrigin",
                      corsPolicyBuilder =>
                      {
                          corsPolicyBuilder.AllowAnyOrigin()
                                           .AllowAnyMethod()
                                           .AllowAnyHeader();
                      });
});

builder.WebHost.UseKestrel()
       .UseContentRoot(Directory.GetCurrentDirectory())
       .UseUrls("http://*:6320", "http://*:5179")
       .UseIISIntegration();

builder.Services.AddScoped<ITimeService, TimeService>();

builder.Services.AddScoped<IEconomyPlanService, EconomyPlanService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IIncomeService, IncomeService>();
builder.Services.AddScoped<IHouseholdService, HouseholdService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

RegisterRepository(builder.Services, builder.Configuration);

var app = builder.Build();

app.UseCors("AllowBlazorOrigin");

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

static void RegisterRepository(IServiceCollection services, IConfiguration config)
{
    var connectionString = config.GetConnectionString("Domain");

    if (string.IsNullOrWhiteSpace(connectionString))
        throw new InvalidOperationException("A connection string needs to be provided");

    services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString,
                                                                           b => b.MigrationsAssembly("EconomyPlannerMigrationDummy")));
}