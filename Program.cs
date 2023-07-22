using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using QuizApplication.Context;
using QuizApplication.Repositories.Implementation;
using QuizApplication.Repositories.Interface;
using QuizApplication.Service.Implementation;
using QuizApplication.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionService, QuestionService>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 10;
    config.IsDismissable = true;
    config.Position = NotyfPosition.TopRight;
});

builder.Services.AddDbContext<QuizApplicationContext>(option =>
    option.UseMySQL(builder.Configuration.GetConnectionString("QuizApplicationContext")));
builder.Services.AddScoped<DbInitializer>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(config =>
               {
                   config.LoginPath = "/home/login";
                   config.Cookie.Name = "QuizApplication";
                   config.ExpireTimeSpan = TimeSpan.FromHours(1);
                   config.AccessDeniedPath = "/home/privacy";
               });
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.SeedToDatabase();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseNotyf();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
