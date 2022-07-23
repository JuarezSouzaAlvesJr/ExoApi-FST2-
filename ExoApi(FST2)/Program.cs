using ExoApi_FST2_.Contexts;
using ExoApi_FST2_.Interfaces;
using ExoApi_FST2_.Repositories;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Adição do cors com criação de nova política
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy => //"CorsPolicy" - nome da política
    {
        policy.WithOrigins("http://localhost/3000") //indicação do local de origem que pode consumir a API (apenas essa url é permitida)
        .AllowAnyHeader() //permitido qualquer header
        .AllowAnyMethod(); //permitido qualquer método
    });
});

//Definir a forma de autenticação
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
}).AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // Valida quem está solicitando
        ValidateIssuer = true,
        // Valida quem está recebendo
        ValidateAudience = true,
        // Define se o tempo de expiração será validado
        ValidateLifetime = true,
        // criptografia e validação da chave de autenticação
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("exoapi-chave-autenticacao")),
        // Valida o tempo de expiração do token
        ClockSkew = TimeSpan.FromMinutes(30),
        // Nome do issuer, de onde está vindo
        ValidIssuer = "exoapi.webapi",
        // Nome do audience, para onde está indo
        ValidAudience = "exoapi.webapi"
    };
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
