using ExoApi_FST2_.Contexts;
using ExoApi_FST2_.Interfaces;
using ExoApi_FST2_.Repositories;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Adi��o do cors com cria��o de nova pol�tica
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy => //"CorsPolicy" - nome da pol�tica
    {
        policy.WithOrigins("http://localhost/3000") //indica��o do local de origem que pode consumir a API (apenas essa url � permitida)
        .AllowAnyHeader() //permitido qualquer header
        .AllowAnyMethod(); //permitido qualquer m�todo
    });
});

//Definir a forma de autentica��o
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
}).AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // Valida quem est� solicitando
        ValidateIssuer = true,
        // Valida quem est� recebendo
        ValidateAudience = true,
        // Define se o tempo de expira��o ser� validado
        ValidateLifetime = true,
        // criptografia e valida��o da chave de autentica��o
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("exoapi-chave-autenticacao")),
        // Valida o tempo de expira��o do token
        ClockSkew = TimeSpan.FromMinutes(30),
        // Nome do issuer, de onde est� vindo
        ValidIssuer = "exoapi.webapi",
        // Nome do audience, para onde est� indo
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
