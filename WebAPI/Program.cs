using Serilog;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

ServiceCollectionExtension.RegisterService(builder.Services, builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Log.Logger = new LoggerConfiguration()
            .WriteTo.MSSqlServer(
                connectionString: builder.Configuration.GetConnectionString("SqlConnection"),
                sinkOptions: new SinkOptions
                {
                    TableName = "Logs",
                    AutoCreateSqlTable = true
                })
            .CreateLogger();
builder.Services.AddSingleton(Log.Logger);
var logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(builder.Configuration)
                        .Enrich.FromLogContext()
                        .CreateLogger();
builder.Host.UseSerilog(logger);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseSerilogRequestLogging();
app.MapControllers();

app.Run();
