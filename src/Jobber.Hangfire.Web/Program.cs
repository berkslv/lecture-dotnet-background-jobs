using Hangfire;
using Hangfire.PostgreSql;
using Jobber.Hangfire.Web.Jobs;
using Jobber.Hangfire.Web.Model;
using TimeZoneConverter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ITestService, TestService>();

builder.Services.AddHangfire(config => {
    config
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UsePostgreSqlStorage(builder.Configuration.GetConnectionString("HangfireConnection"));

    var cronEveryMinute = "*/1 * * * *";
    var recurringJobOptions = new RecurringJobOptions
    {
        TimeZone = TZConvert.GetTimeZoneInfo("Etc/GMT+3")
    };
    RecurringJob.AddOrUpdate<ITestService>("id-run-and-wait", x => x.RunTests(Guid.NewGuid(), TestType.Recurring, CancellationToken.None), cronEveryMinute, recurringJobOptions);
});

builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard();
app.MapHangfireDashboard();

app.Run();
