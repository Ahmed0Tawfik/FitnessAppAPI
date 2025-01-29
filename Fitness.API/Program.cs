using Fitness.API.DependencyInjection;
using Fitness.API.EndPoints;
using Fitness.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// ADDING DEPENDENCIES
builder.Services.AddAutoRegisterHandlers();
builder.Services.AddFluentValidation();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// MIDDLEWARE
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<ValidationMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// MAPPING ENDPOINTS
app.MapAuthEndPoints();

app.Run();