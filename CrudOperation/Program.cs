using CrudOperation.Data;
using CrudOperation.Models;
using CrudOperation.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(op =>
{
    op.UseSqlServer(builder.Configuration.GetConnectionString("Con"));
});
//builder.Services.AddScoped(typeof(IRepository<Patient>), typeof(Repository<Patient>));
//builder.Services.AddScoped(typeof(IRepository<Appointment>), typeof(Repository<Appointment>));
//builder.Services.AddScoped(typeof(IRepository<Doctor>), typeof(Repository<Doctor>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
