using Microsoft.EntityFrameworkCore;
using Quiz.QuestionStorage;
using Quiz.QuestionStorage.Db;
using Quiz.QuestionStorage.Services;

var builder = WebApplication.CreateBuilder(args);

// TODO: вынести в конфигурацию.
builder.Services.AddDbContext<Context>((serviceProvider, act) =>
{
	var conf = serviceProvider.GetRequiredService<IConfiguration>();
	var connectionString = conf.GetValue<string>("ConnectionStrings:Database");
	var serverVersion = ServerVersion.Parse("8.0.29");
	act.UseMySql(connectionString, serverVersion);
});
builder.Services.AddAutoMapper(config => config.AddProfile(typeof(AutoMapperProfile)));
builder.Services.AddControllers();
builder.Services.AddScoped<IQuestionService, QuestionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();
app.MapGet("/",
	() => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();