using Kanban;
using Kanban.Services;
using Kanban.Services.AddCard;
using Kanban.Services.AddCardList;
using Kanban.Services.CreateBoard;
using Kanban.Services.GetAllBoards;
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
builder.Services.AddMudServices();

await builder.Build().RunAsync();
