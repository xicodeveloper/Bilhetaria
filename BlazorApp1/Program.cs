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
// using (var scope = app.Services.CreateScope())
// {
//     var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

//     if (!context.Movies.Any())
//     {
//         context.Movies.AddRange(new[]
//         {
//             new Film {
//                 Title = "Inception",
//                 ReleaseDate = new DateTime(2010, 7, 16),
//                 BackdropPath = "/backdrop_inception.jpg",
//                 PosterPath = "/poster_inception.jpg",
//                 Overview = "A thief who steals corporate secrets...",
//                 Popularity = 8.8,
//                 VoteAverage = 8.3,
//                 VoteCount = 21000,
//                 OriginalTitle = "Inception",
//                 OriginalLanguage = "en",
//                 Adult = false,
//                 Video = false
//             },
//             new Film {
//                 Title = "The Matrix",
//                 ReleaseDate = new DateTime(1999, 3, 31),
//                 BackdropPath = "/backdrop_matrix.jpg",
//                 PosterPath = "/poster_matrix.jpg",
//                 Overview = "A computer hacker learns from mysterious rebels...",
//                 Popularity = 9.1,
//                 VoteAverage = 8.7,
//                 VoteCount = 17000,
//                 OriginalTitle = "The Matrix",
//                 OriginalLanguage = "en",
//                 Adult = false,
//                 Video = false
//             }
//         });

//         await context.SaveChangesAsync();
//         Console.WriteLine("[DEBUG] Filmes inseridos na BD com sucesso.");
//     }
//     else
//     {
//         Console.WriteLine("[DEBUG] Filmes já existem na base de dados.");
//     }
// }


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