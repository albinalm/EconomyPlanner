using EconomyPlanner.Abstractions.Configuration;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.RegisterRepository(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddCors(options =>
// {
//     options.AddDefaultPolicy(corsPolicyBuilder => 
//                                  corsPolicyBuilder.WithOrigins("https://localhost:44338")
//                                         .AllowAnyMethod()
//                                         .AllowAnyHeader());
// }); 
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowBlazorOrigin",
                      corsPolicyBuilder =>
                      {
                          corsPolicyBuilder.WithOrigins("http://localhost:5179", "http://localhost:5114")
                                           .AllowAnyMethod()
                                           .AllowAnyHeader();
                      });
});
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