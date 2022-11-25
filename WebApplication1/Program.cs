using WebApplication1;
var builder = WebApplication.CreateBuilder(args);
var startup = new StartUp(builder.Configuration);
startup.ConfigureServices(builder.Services);

// Add services to the container.

builder.Services.AddSwaggerGen();
builder.Services.AddMvcCore().AddApiExplorer();
var app = builder.Build();
//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
startup.Configure(app, app.Environment);



app.Run();
