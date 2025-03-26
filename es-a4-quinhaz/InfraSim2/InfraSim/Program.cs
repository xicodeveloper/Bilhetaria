// regista razor components, traffic routing service, and server implementation
// to the DI container and configure the HTTP request pipeline for the app 

// using InfraSim.Components;
// using InfraSim.Services.Traffic.Classes;
// using InfraSim.Services.Traffic.Interfaces;
// //using InfraSim.Services.Traffic.Enums;
// //using System.Collections.Generic;

// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// builder.Services.AddRazorComponents()
//     .AddInteractiveServerComponents();

// // Register the TrafficCoordinator service
// builder.Services.AddSingleton<TrafficCoordinator>();
// // Register an empty list of IServer as a singleton
// builder.Services.AddSingleton<List<IServer>>(new List<IServer>());
// {
//     var servers = provider.GetRequiredService<List<IServer>>();
//     return new FullTrafficRouting(servers, ServerType.Server); // Pass the server type
// };

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Error", createScopeForErrors: true);
//     app.UseHsts();
// }

// app.UseHttpsRedirection();
// app.UseStaticFiles();
// app.UseAntiforgery();

// app.MapStaticAssets();
// app.MapRazorComponents<App>()
//     .AddInteractiveServerRenderMode();

// app.Run();


using InfraSim.Components;
using InfraSim.Services.Traffic.Classes;
using InfraSim.Services.Traffic.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register services
builder.Services.AddSingleton<TrafficCoordinator>();
builder.Services.AddSingleton<List<IServer>>(new List<IServer>());

var app = builder.Build();

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