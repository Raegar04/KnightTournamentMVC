using KnightTournament.BLL.Abstractions;
using KnightTournament.BLL.Implementations;
using KnightTournament.DAL;
using KnightTournament.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<LocationService>();
builder.Services.AddScoped<TrophyService>();
builder.Services.AddScoped(typeof(ISqlService<>), typeof(SqlService<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<TournamentService>();
builder.Services.AddScoped<RoundService>();
builder.Services.AddScoped<CombatService>();
builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddUserStore<UserStore<AppUser, AppRole, ApplicationDbContext, Guid>>();

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
//        defaults: new { controller = "Round" }
//    );
//});

app.Run();
