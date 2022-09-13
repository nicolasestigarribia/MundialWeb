global using Blazored.SessionStorage;
using Microsoft.EntityFrameworkCore;
using Mundial.EF;
using MudBlazor.Services;
using Mundial.Web.Services;
using Mundial.Web.Auth;
using TLogger;
using Serilog;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDbContext<Mundial2022Context>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("Ares")));


builder.Services.AddHttpClient<IEstadioService, EstadioService>(Estadio =>
    Estadio.BaseAddress = new Uri(builder.Configuration.GetSection("Api:Urls:Mundial").Value));

builder.Services.AddHttpClient<IEquipoService, EquipoService>(Equipo =>
    Equipo.BaseAddress = new Uri(builder.Configuration.GetSection("Api:Urls:Mundial").Value));

builder.Services.AddHttpClient<IPreguntaService, PreguntaService>(Pregunta =>
    Pregunta.BaseAddress = new Uri(builder.Configuration.GetSection("Api:Urls:Mundial").Value));

builder.Services.AddHttpClient<IRespuestaCompuestasService, RespuestaCompuestaService>(Respuesta =>
    Respuesta.BaseAddress = new Uri(builder.Configuration.GetSection("Api:Urls:Mundial").Value));

builder.Services.AddHttpClient<IRespuestaSimpleService, RespuestaSimpleService>(Respuesta =>
    Respuesta.BaseAddress = new Uri(builder.Configuration.GetSection("Api:Urls:Mundial").Value));

builder.Services.AddHttpClient<IJugadorService, JugadorService>(Jugador=>
    Jugador.BaseAddress = new Uri(builder.Configuration.GetSection("Api:Urls:Mundial").Value));

builder.Services.AddHttpClient<ILoginService, LoginService>(Login =>
    Login.BaseAddress = new Uri(builder.Configuration.GetSection("Api:Urls:Mundial").Value));

builder.Services.AddHttpClient<IPuntajeService, PuntajeService>(Puntaje =>
    Puntaje.BaseAddress = new Uri(builder.Configuration.GetSection("Api:Urls:Mundial").Value));

builder.Services.AddHttpClient<IUsuarioService, UsuarioService>(Usuario =>
    Usuario.BaseAddress = new Uri(builder.Configuration.GetSection("Api:Urls:Mundial").Value));

builder.Services.AddHttpClient<IEmpresaService, EmpresaService>(Empresa =>
    Empresa.BaseAddress = new Uri(builder.Configuration.GetSection("Api:Urls:Mundial").Value));

builder.Services.AddHttpClient<IPersonaService, PersonaService>(Persona =>
    Persona.BaseAddress = new Uri(builder.Configuration.GetSection("Api:Urls:Mundial").Value));

builder.Services.AddHttpClient<IClubService, ClubService>(Club =>
    Club.BaseAddress = new Uri(builder.Configuration.GetSection("Api:Urls:Mundial").Value));

builder.Services.AddHttpClient<IDeporteService, DeporteService>(Deporte =>
    Deporte.BaseAddress = new Uri(builder.Configuration.GetSection("Api:Urls:Mundial").Value));

builder.Services.AddHttpClient<IHobbyService, HobbyService>(Hobby =>
    Hobby.BaseAddress = new Uri(builder.Configuration.GetSection("Api:Urls:Mundial").Value));

builder.Services.AddHttpClient<IProveedorService, ProveedorService>(Proveedor =>
    Proveedor.BaseAddress = new Uri(builder.Configuration.GetSection("Api:Urls:Mundial").Value));

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProvider>(provider => provider.GetRequiredService<AuthenticationProvider>());

builder.Services.AddBlazoredSessionStorage();

builder.Services.AddMudServices();
builder.Services.AddMudBlazorDialog();
builder.Services.AddMudBlazorSnackbar();
builder.Services.AddMudBlazorResizeListener();

string name = typeof(Program).Assembly.GetName().Name;
Log.Logger = TLogger.Serilog.GetLogger(name);
builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
