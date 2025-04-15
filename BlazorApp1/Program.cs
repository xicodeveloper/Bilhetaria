using BlazorApp1.Components;
using BlazorApp1.Services.DataBase;
using BlazorApp1.Services.Movies;
using BlazorApp1.Services.RegLogin;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using MySql.Data.MySqlClient;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;


Env.Load();

var builder = WebApplication.CreateBuilder(args);

// 1. Configuração única da autenticação
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.Cookie.Name = "auth_token";
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/access-denied";
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("VerifiedEmail", policy => 
        policy.RequireClaim("EmailConfirmed", "true"));
});
// 2. Configuração da autorização
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Authenticated", policy =>
        policy.RequireAuthenticatedUser());
});

// 3. Configuração do MySQL
try
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    });
    Console.WriteLine("Conexão com o banco de dados estabelecida");
}
catch (Exception error)
{
    Console.WriteLine("Erro ao conectar com o banco de dados: "+ error.StackTrace);
}
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<MovieDeserializer>();

// 4. Registro dos serviços
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<VerificationService>();
builder.Services.AddScoped<EmailService>();  // Registrar o EmailService

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

// 5. Configuração do Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 6. Configuração do Kestrel
builder.WebHost.UseUrls("https://localhost:7193", "http://localhost:5212");
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(7193, listenOptions => listenOptions.UseHttps());
    serverOptions.ListenAnyIP(5212);
});

var app = builder.Build();

// 7. Ordem CORRETA dos middlewares
app.UseAuthentication();
app.UseAuthorization();

// Configuração do pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();