using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Zawamil.Core.Interface.ISongServices;
using Zawamil.Core.Interface.IUserServices;
using Zawamil.EF;
using Zawamil.EF.SondServices;
using Zawamil.EF.UserServices;

var builder = WebApplication.CreateBuilder(args);
string MyAllowSpecificOrigins = "_MaAllowSpecificOrigins";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
     b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
);

// Add services to the container.
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ISongService, SongService>();

builder.Services.AddControllers();
builder.Services.AddApiVersioning(opt =>
{
    opt.AssumeDefaultVersionWhenUnspecified = true;
    //opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1,0);
    opt.DefaultApiVersion = ApiVersion.Default;

    opt.ReportApiVersions = true;

    opt.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("x-version"),
        new MediaTypeApiVersionReader("ver")
        );
});


builder.Services.AddCors(options =>
options.AddPolicy(name: MyAllowSpecificOrigins,
    builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    })
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors(
    builder => builder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .SetIsOriginAllowed((host) => true)
    .AllowCredentials());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();
app.UseAuthentication();


app.MapControllers();

app.Run();
