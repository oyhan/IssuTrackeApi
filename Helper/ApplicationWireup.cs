using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Helper
{
    public class ApplicationWirup
    {
        public static void WireupSerilog()
        {
            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
            var columnOptions = new ColumnOptions();
            columnOptions.Store.Remove(StandardColumn.Properties);
            columnOptions.LogEvent.ExcludeAdditionalProperties = true;
            // we do want JSON data
            columnOptions.Store.Add(StandardColumn.LogEvent);

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                //.WriteTo.Console()
                .WriteTo.MSSqlServer(
                    connectionString: config.GetConnectionString("DefaultConnection"),
                    autoCreateSqlTable: true,
                    tableName: "Logs", period: TimeSpan.FromSeconds(5),
                    columnOptions: columnOptions
                )
                .CreateLogger();

        }
    }
}
