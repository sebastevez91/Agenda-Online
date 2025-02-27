using AgendaOnline.Data;
using AgendaOnline.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar conexi�n a la base de datos
builder.Services.AddDbContext<AgendaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar Identity con soporte para roles
builder.Services.AddDefaultIdentity<Users>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AgendaDbContext>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Crear roles por defecto al iniciar la aplicación
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await CrearRoles(services);
}

app.UseStaticFiles();

app.MapRazorPages();

app.Run();

// Método para crear roles
async Task CrearRoles(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = { "Admin", "Usuario" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}