using ToDoApp.Data;
using ToDoApp.Data.Enums;
using ToDoApp.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<SqlContext>();
builder.Services.AddSingleton<XmlContext>();

builder.Services.AddScoped<ServiceFactory>();

builder.Services.AddKeyedScoped<INoteService, NoteServiceSql>(StorageType.Sql);
builder.Services.AddKeyedScoped<INoteService, NoteServiceXml>(StorageType.Xml);

builder.Services.AddKeyedScoped<ICategoryService, CategoryServiceSql>(StorageType.Sql);
builder.Services.AddKeyedScoped<ICategoryService, CategoryServiceXml>(StorageType.Xml);

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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Note}/{action=Index}/{id?}");

app.Run();
