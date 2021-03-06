using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SajhaSabal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SsdbContext>(op =>
{
    op.UseMySql("server=localhost;uid=root;pwd=password;database=sajhasabal", ServerVersion.AutoDetect("server=localhost;uid=root;pwd=password;database=sajhasabal"));
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<SsdbContext>()
    .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(op =>
{
    op.Password.RequireNonAlphanumeric = false;
    op.Password.RequireLowercase = false;
});
builder.Services.AddSession();

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

app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();