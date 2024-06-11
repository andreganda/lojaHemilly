using lojaHemilly.DataBase;
using lojaHemilly.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Adicionar o serviço do DbContext com MySQL
builder.Services.AddDbContext<FlorDeLizContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 25))));


// Registrando o serviço. 
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IVendaService, VendaService>();
builder.Services.AddScoped<IItemVendaService, ItemVendaService>();
builder.Services.AddScoped<IParcelaService, ParcelaService>();






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


// Configure additional static files middleware to serve from a different directory
var externalStaticFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "Views");
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(externalStaticFilesPath),
    RequestPath = "/Views"
});


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
		pattern: "{controller=Vendas}/{action=Index}/{id?}");
//pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
