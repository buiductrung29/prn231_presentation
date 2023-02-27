using JWT_Sample.Repositories;
using JWT_Sample.Repositories.Implements;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
