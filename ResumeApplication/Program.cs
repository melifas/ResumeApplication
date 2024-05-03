using Microsoft.EntityFrameworkCore;
using ResumeApplication;
using ResumeApplication.Context;
using ResumeApplication.Interfaces;
using ResumeApplication.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ICandidateService, CandidateService>();

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(builder.Configuration.GetConnectionString(Constants.ResumeDb)));

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

app.Run();
