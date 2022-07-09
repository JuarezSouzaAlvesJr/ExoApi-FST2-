using ExoApi_FST2_.Contexts;
using ExoApi_FST2_.Interfaces;
using ExoApi_FST2_.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Adição do cors com criação de nova política
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy => //"CorsPolicy" - nome da política
    {
        policy.WithOrigins("http://localhost/7130") //indicação do local de origem que pode consumir a API (apenas essa url é permitida)
        .AllowAnyHeader() //permitido qualquer header
        .AllowAnyMethod(); //permitido qualquer método
    });
});

builder.Services.AddScoped<ExoApiContext, ExoApiContext>();

builder.Services.AddTransient<ProjetoRepository, ProjetoRepository>();

builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy"); //Geralmente, deve ficar acima do Authorization

app.UseAuthorization();

app.MapControllers();

app.Run();
