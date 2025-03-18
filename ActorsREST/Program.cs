using ActorLib;

var builder = WebApplication.CreateBuilder(args);

const string AllowAll = "AllowAll";

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<ActorsRepository>(new ActorsRepository());
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowAll,
                              policy =>
                              {
                                  policy.AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader();
                              });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.UseCors(AllowAll);

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();

