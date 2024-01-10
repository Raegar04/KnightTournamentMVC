using KnightTournament.BLL.Abstractions;
using KnightTournament.BLL.Implementations;
using KnightTournament.DAL;
using KnightTournament.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options=>options.UseLazyLoadingProxies(true).UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<LocationService>();
builder.Services.AddScoped<TrophyService>();
builder.Services.AddScoped(typeof(ISqlService<>), typeof(SqlService<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<TournamentService>();
builder.Services.AddScoped<RoundService>();
builder.Services.AddScoped<CombatService>();
builder.Services.AddScoped<TournamentUserService>();
builder.Services.AddScoped<CombatKnightService>();
builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>().AddEntityFrameworkStores<ApplicationDbContext>().AddUserStore<UserStore<AppUser, IdentityRole<Guid>, ApplicationDbContext, Guid>>();

var app = builder.Build();

Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//        name: "rounds",
//        pattern: "tournament/{tournamentId}/round/{action=Index}/{id?}",
//        defaults: new { controller = "Trophy_Round" }
//    );
//});

app.Run();
