using Assembly.Database;
using Assembly.Service;
using Assembly.CrossCutting;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Xml.Linq;
using System.Data;
using Assembly.Domain;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages();

//**********************************************************************
//********************************************** grupo autentuicacao
// Data Protection and PasswordHasher
builder.Services.AddDataProtection();
//builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddHttpContextAccessor();
//*****************************************

//************************************************************* grupo site
// adciona outro servico classe croscutting todos os serviço la   ---   IOC
builder.Services.AddRepos();
builder.Services.AddServices();
//builder.Services.AddRazorPages();
//**********************************************************************

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        //options.Cookie.Name = "AuthAssembly";
        //options.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;

        options.LoginPath = "/Login/Login";
        options.LogoutPath = "/Login/Logout";
        options.AccessDeniedPath = "/Login/Register";
    });


builder.Services.AddAuthorization(options =>
{

    options.AddPolicy("Master", pb =>
    {
        pb.RequireAuthenticatedUser()
            .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
            .RequireClaim("role", TipoUsuarioEnum.Master.ToString());
    });

    options.AddPolicy("Admin", pb =>
    {
        pb.RequireAuthenticatedUser()
            .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
            .RequireClaim("role", TipoUsuarioEnum.Master.ToString(), TipoUsuarioEnum.Admin.ToString());
    });

    options.AddPolicy("Gerente", pb =>
    {
        pb.RequireAuthenticatedUser()
            .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
            .RequireClaim("role", TipoUsuarioEnum.Master.ToString(), TipoUsuarioEnum.Admin.ToString(), 
             TipoUsuarioEnum.Gerente.ToString());
    });

    options.AddPolicy("Usuario", pb =>
    {
        pb.RequireAuthenticatedUser()
            .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
            .RequireClaim("role", TipoUsuarioEnum.Master.ToString(), TipoUsuarioEnum.Admin.ToString(),
             TipoUsuarioEnum.Gerente.ToString(), TipoUsuarioEnum.Usuario.ToString());
    });

    // todos logados
    options.AddPolicy("LoggedIn", pb =>
    {
        pb.RequireAuthenticatedUser()
            .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme);
    });


    //nao esta funconando estuda depoois
    //options.AddPolicy("user", pb =>
    //{
    //    pb.RequireAuthenticatedUser()
    //        .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
    //        .AddRequirements(new UserRequirement());
    //});
});


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

// padrao liguagem  *********************************************** INCUIDO
/*
var sCulturas = new[] {new CultureInfo ( name: "pt -BR")};
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(culture: "pt -BR", uiCulture: "pt -BR"),
    SupportedCultures = sCulturas,
    SupportedUICultures = sCulturas
});
*/

app.UseRouting();
app.UseAuthentication(); // Who is the person
app.UseAuthorization(); // What the logged in person can do

app.MapRazorPages();

app.Run();
