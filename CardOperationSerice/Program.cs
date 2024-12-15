using CardOperationSerice.Middleware;
using CardOperationSerice.Repositories.Implementation;
using CardOperationSerice.Repositories.Interfaces;
using CardOperationSerice.Services.Abstract;
using CardOperationSerice.Services.Implementation;
using CardOperationSerice.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSingleton<ICardRepository, CardRepository>();
builder.Services.AddSingleton<ICardService, CardService>();
builder.Services.AddSingleton<IRuleService, RuleService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();
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
