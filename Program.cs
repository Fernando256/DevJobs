using DevJobs.API.Persistence;
using DevJobs.API.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DevJobsCs");
// builder.Services.AddDbContext<DevJobsContext>(options => 
//     options.UseSqlServer(connectionString)
// );

builder.Services.AddDbContext<DevJobsContext>(options => 
    options.UseInMemoryDatabase("DevJobs")
);
builder.Services.AddScoped<IJobVacancyRepository, JobVacancyRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo{
        Title = "DevJobs.API",
        Version = "v1",
        Contact = new OpenApiContact {
            Name = "Luis Fernando",
            Email = "luisfernando_paganini@hotmail.com",
            Url = new Uri("https://www.linkedin.com/in/luis-fernando-paganini-68763b1a9/")
        }
    });

    var xmlFile = "DevJobs.API.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    c.IncludeXmlComments(xmlPath);
});

// builder.Host.ConfigureAppConfiguration((hostingContext, config) => {
//     Serilog.Log.Logger = new LoggerConfiguration()
//         .Enrich.FromLogContext()
//         .WriteTo.MSSqlServer(connectionString,
//             sinkOptions: new MSSqlServerSinkOptions () {
//                 AutoCreateSqlTable = true,
//                 TableName = "Logs"
//             })
//         .WriteTo.Console()
//         .CreateLogger();
// }).UseSerilog();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
