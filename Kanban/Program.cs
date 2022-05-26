using Kanban;
using Kanban.Services;
using Kanban.Services.AddCard;
using Kanban.Services.AddCardList;
using Kanban.Services.CreateBoard;
using Kanban.Services.GetAllBoards;
using Kanban.Services.GetBoard;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<KanbanApi>();
builder.Services.AddScoped<AddCardService>();
builder.Services.AddScoped<AddCardListService>();
builder.Services.AddScoped<CreateBoardService>();
builder.Services.AddScoped<GetAllBoardsService>();
builder.Services.AddScoped<GetBoardService>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddMudServices();



var host = builder.Build();

var authenticationService = host.Services.GetRequiredService<AuthenticationService>();
await authenticationService.Initialize(); // necessary because you can't run async methods inside the RouteView.Render()

await host.RunAsync();