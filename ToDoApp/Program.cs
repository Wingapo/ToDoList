using GraphQL;
using GraphQL.Server.Ui.Altair;
using ToDoApp.Data;
using ToDoApp.Data.Enums;
using ToDoApp.Data.Services;
using ToDoApp.GraphQl.Mutations;
using ToDoApp.GraphQl.Queries;
using ToDoApp.GraphQl.Schema;
using ToDoApp.GraphQl.Types;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<SqlContext>();
builder.Services.AddSingleton<XmlContext>();

builder.Services.AddSingleton<ServiceFactory>();

builder.Services.AddKeyedTransient<INoteService, NoteServiceSql>(StorageType.Sql);
builder.Services.AddKeyedTransient<INoteService, NoteServiceXml>(StorageType.Xml);

builder.Services.AddKeyedTransient<ICategoryService, CategoryServiceSql>(StorageType.Sql);
builder.Services.AddKeyedTransient<ICategoryService, CategoryServiceXml>(StorageType.Xml);

builder.Services.AddSingleton<CategoryType>();
builder.Services.AddTransient<CategoryQuery>();
builder.Services.AddSingleton<CategoryInputType>();
builder.Services.AddTransient<CategoryMutation>();

builder.Services.AddSingleton<NoteType>();
builder.Services.AddTransient<NoteQuery>();
builder.Services.AddSingleton<NoteInputType>();
builder.Services.AddTransient<NoteMutation>();

builder.Services.AddTransient<RootQuery>();
builder.Services.AddTransient<RootMutation>();
builder.Services.AddSingleton<RootSchema>();

builder.Services.AddGraphQL(graphBuilder => graphBuilder
    .AddSchema<RootSchema>()
    .AddSystemTextJson());


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

app.UseGraphQL();
app.UseGraphQLAltair(options: new AltairOptions
{
    GraphQLEndPoint = "/graphql",
    SubscriptionsEndPoint = "/graphql",
    Headers = new Dictionary<string, string>
    {
        {"Storage", ((int)StorageType.Sql).ToString()}
    }
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Note}/{action=Index}/{id?}");

app.Run();
