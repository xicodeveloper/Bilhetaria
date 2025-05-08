using BlazorApp1.Components;
using BlazorApp1.Services.DataBase;
using BlazorApp1.Services.Movies;
using BlazorApp1.Services.Purchase.OrderState;
using BlazorApp1.Services.RegLogin;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
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

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Authenticated", policy =>
        policy.RequireAuthenticatedUser());
});




//builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
  //  options.UseSqlite("Data Source=Data/app.db"));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=Data/app.db"));

// Injeção de dependência
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<MovieDeserializer>();

// 4. Registro dos serviços
// Adicione estas linhas:
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => 
    sp.GetRequiredService<CustomAuthStateProvider>());
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<VerificationService>();
builder.Services.AddScoped<EmailService>();  // Registrar o EmailService
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => 
    sp.GetRequiredService<CustomAuthStateProvider>());
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

// 5. Configuração do Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IStateFactory, StateFactory>(); 
// 6. Configuração do Kestrel
builder.WebHost.UseUrls("https://localhost:7193", "http://localhost:5212");
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(7193, listenOptions => listenOptions.UseHttps());
    serverOptions.ListenAnyIP(5212);
});

var app = builder.Build();


app.UseAuthentication();
app.UseAuthorization();


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