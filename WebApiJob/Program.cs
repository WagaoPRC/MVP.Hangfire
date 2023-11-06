using Hangfire;
using MyCronJob;


//Please add a NuGet package reference to either 'Microsoft.Data.SqlClient' or 'System.Data.SqlClient' in your application project. Hangfire.SqlServer supports both providers but let the consumer decide which one should be used.'
using System.Data.SqlClient;
using WebApiJobs.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Connection Context
var connection = builder.Configuration.GetConnectionString("localhost");

//Hangfire
builder.Services.AddHangfire(
    op => op
    .UseSqlServerStorage(connection) // Set SqlServer and Sql ConnectionString
    .UseRecommendedSerializerSettings() // Use Defatul Json to serialize, can change setting passing a JsonSerializerSettings object;
    );
builder.Services.AddHangfireServer();

//Extensions Workers And Jobs
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.X
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 5 }); // Set Default Try Attempts
app.UseHangfireDashboard();

BackgroundJob.Enqueue<Worker>(x => x.MyJobFireAndForget());
//BackgroundJob.Enqueue<Worker>(x => x.MyFirstExceptionJob());
RecurringJob.AddOrUpdate<Worker>(x => x.MyRecurringJob(), cronExpression: Cron.Hourly); //Obsolete
BackgroundJob.Schedule<Worker>(x => x.MyDelayedjob(), TimeSpan.FromDays(2));

//Continuations Job
string jobId = BackgroundJob.Enqueue<Worker>(x => x.MyFatherJob());
BackgroundJob.Enqueue<Worker>(jobId,x => x.MySunJob());


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();