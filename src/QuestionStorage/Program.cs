using Microsoft.EntityFrameworkCore;
using Quiz.QuestionStorage;
using Quiz.QuestionStorage.Db;
using Quiz.QuestionStorage.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>((serviceProvider, act) =>
{
	var conf = serviceProvider.GetRequiredService<IConfiguration>();
	var connectionString = conf.GetValue<string>("ConnectionStrings:Database");
	var serverVersion = ServerVersion.Parse("8.0.29");
	act.UseMySql(connectionString, serverVersion);
});
builder.Services.AddAutoMapper(config => config.AddProfile(typeof(AutoMapperProfile)));
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

var app = builder.Build();

app.MapGrpcService<GrpcApi>();
if (app.Environment.IsDevelopment())
{
	app.MapGrpcReflectionService();
}

app.Run();