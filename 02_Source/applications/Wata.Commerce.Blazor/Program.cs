using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using MudBlazor.Services;
using Blazored.Modal.Services;
using Wata.Commerce.Common.Middlewares;
using Wata.Commerce.Common.Helpers;
using Wata.Commerce.Common.Objects;
using Wata.Commerce.Common.Component.Services;
using Wata.Commerce.Sample.Client.Services;
using Wata.Commerce.Sample.Module.Services;
using Wata.Commerce.Sample.Module.MapperProfiles;
using Wata.Commerce.Sample.Module.Models.Abc;

var builder = WebApplication.CreateBuilder(args);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IModalService, ModalService>();
builder.Services.AddScoped<SpinnerService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

//Clients
builder.Services.AddScoped<IAbcClient, AbcClient>();

//Business
//builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IAbcService, AbcService>();

// pager
builder.Services.AddScoped<IPageHelper, PageHelper>();

builder.Services.AddScoped<EditSuccess>();

// filters
builder.Services.AddScoped<IAbcFilters, AbcGridControls>();

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

app.UseMiddleware<ExceptionHandler>();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();