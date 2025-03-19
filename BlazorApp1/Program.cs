using BlazorApp1.Components;
using DotNetEnv;
using MySql.Data.MySqlClient;
using BlazorApp1.Services.RegLogin;
using Microsoft.AspNetCore.Components.Authorization; // Ensure this using statement is present

// ... other code


//carregar o fichero .env
Env.Load();

// Add services to the container.
var builder = WebApplication.CreateBuilder(args);
// Configurar conexão MySQL

builder.Services.AddScoped<Sign>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddHttpContextAccessor();



try
{
    using var connection = new MySqlConnection("Server=localhost;port=3306;database=ticketzone;Uid=root;Pwd=''");
    connection.Open();
    Console.WriteLine("✅ Conexão MySQL estabelecida com sucesso!");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Erro ao conectar ao MySQL: {ex.Message}");
}

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.WebHost.UseUrls("https://localhost:7193", "http://localhost:5212");
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(7193, listenOptions => listenOptions.UseHttps());
    serverOptions.ListenAnyIP(5212);
});

var app = builder.Build();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();