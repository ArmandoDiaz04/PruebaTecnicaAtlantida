using API.Core.Application;
using API.Infraestructure;
using API.MiddlewareException;

var builder = WebApplication.CreateBuilder(args);
const string namePolicy = "CorsPolicy";
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

InfraestructureServices.AddInfrastructure(builder.Services, builder.Configuration);
ApplicationServices.AddApplication(builder.Services);

builder.Services.AddCors(options =>
{
    options.AddPolicy(namePolicy, builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<MiddlewareHandlerException>();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors(namePolicy);

app.MapControllers();

app.Run();

