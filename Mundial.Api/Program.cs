using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Mundial.Negocio;
using Mundial.Negocio.Utils;
using Serilog;
using System.Configuration;
using System.Text;
using TLogger;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<EstadioNegocio>();
builder.Services.AddTransient<EquipoNegocio>();
builder.Services.AddScoped<JugadorNegocio>();
builder.Services.AddTransient<PreguntaNegocio>();
builder.Services.AddTransient<RespuestaCompuestaNegocio>();
builder.Services.AddTransient<RespuestaSimpleNegocio>();
builder.Services.AddTransient<UsuarioRespuestaNegocio>();
builder.Services.AddTransient<UsuarioNegocio>();
builder.Services.AddTransient<EmpresaNegocio>();
builder.Services.AddTransient<PersonaNegocio>();
builder.Services.AddTransient<HobbyNegocio>();
builder.Services.AddTransient<ClubNegocio>();
builder.Services.AddTransient<DeporteNegocio>();
builder.Services.AddTransient<ProveedorNegocio>();



builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:SecretKey").Value)),
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true
                    });



string name = typeof(Program).Assembly.GetName().Name;
Log.Logger = TLogger.Serilog.GetLogger(name);
builder.Host.UseSerilog();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
