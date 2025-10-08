using CodeProject.Data;
using CodeProject.Interfaces;
using CodeProject.Models;
using CodeProject.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
// For API controllers
builder.Services.AddControllers();

// To enable Swagger for API testing, uncomment this
// Swagger requirement
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
//    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
//    c.IncludeXmlComments(xmlPath);
//});

//Register IApplicationDbContext so that when IApplicationDbContext is asked for, it'll provide ApplicationDbContext
builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
//Register TaskService
builder.Services.AddScoped<ITaskService, TaskService>();
// Setting up in memory db
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("InMemoryDb"));
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    //Pre populate db
    InitialDataLoad(context);
}

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

// To enable Swagger for API testing, uncomment this
//app.UseSwagger();
//app.UseSwaggerUI(c =>
//{
//    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task API");
//    c.RoutePrefix = string.Empty;
//});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.Run();

//This method will pre populate some data in the db
static void InitialDataLoad(ApplicationDbContext context)
{
    if (!context.Tasks.Any())
    {
        context.Tasks.AddRange(
            new TaskModel
            {
                Title = "Task One",
                Description = "This is a task for case one",
                Status = "Pending",
                DueDateTime = DateTime.Now.AddDays(1)
            },
            new TaskModel
            {
                Title = "Task Two",
                Description = "This is a task for case two",
                Status = "Completed",
                DueDateTime = DateTime.Now
            }
        );

        //EF Core will assign an Id to each object before inserting
        context.SaveChanges();
    }
}