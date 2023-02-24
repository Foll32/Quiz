using Microsoft.EntityFrameworkCore;
using Quiz.QuestionStorage.Db;

var builder = WebApplication.CreateBuilder(args);

// TODO: вынести в конфигурацию.
builder.Services.AddDbContext<Context>(act =>
{
	var connectionString = "Server=localhost;Port=33077;Database=question_storage;Uid=root;Pwd=1qazXSW@;";
	var serverVersion = ServerVersion.Parse("8.0.29");
	act.UseMySql(connectionString, serverVersion);
});
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();
app.MapGet("/",
	() => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();