using Zawamil.Web.Service.IService;
using Zawamil.Web.Service;
using Zawamil.Web.Utility;
using Azure.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
SD.BaseUrl = builder.Configuration["ServiceUrls:BaseUrl"];
builder.Services.AddScoped(Sp => new HttpClient { BaseAddress = new Uri("https://zawamilapi.runasp.net/") });
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<IBaseService, BaseService>();

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.ExpireTimeSpan = TimeSpan.FromHours(10);
//        options.LoginPath = "/Home/Login";
//        options.AccessDeniedPath = "/Home/AccessDenied";
//    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
	FileProvider = new PhysicalFileProvider(
		 Path.Combine(builder.Environment.ContentRootPath, "wwwroot")),
	//Path.Combine(Directory.GetCurrentDirectory(),"wwwroot")),
	RequestPath = "/wwwroot",
});
app.UseFileServer(new FileServerOptions
{
	FileProvider = new PhysicalFileProvider(
		Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
	RequestPath = "/wwwroot",
	EnableDefaultFiles = true
});
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
