using Serilog.Events;
using Serilog;
using System.Collections.ObjectModel;
using System.Data;
using Serilog.Sinks.MSSqlServer;

namespace System.Api.Module
{
    public class SerilogModule
    {
    }

    public static class SerilogExpand
    {
        public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
        {
            string strConn = builder.Configuration.GetConnectionString("default");
            var columnOptions = new ColumnOptions
            {
                AdditionalDataColumns = new Collection<DataColumn>
                {
                    new DataColumn {DataType = typeof (string), ColumnName = "RequstUri",AllowDBNull=false},
                    new DataColumn {DataType = typeof (long), ColumnName = "TimeTaken",AllowDBNull=false},
                    new DataColumn {DataType = typeof (string), ColumnName = "EndTimeData",AllowDBNull=true},
                    new DataColumn {DataType = typeof (string), ColumnName = "IP",AllowDBNull=false},
                    new DataColumn {DataType = typeof (string), ColumnName = "RequstParameter",AllowDBNull=true},
                    new DataColumn {DataType = typeof (string), ColumnName = "ResponseResult",AllowDBNull = true},
                }
            };
            //配置日志
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information().MinimumLevel.Override("Microsoft", LogEventLevel.Fatal)
                .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
                .WriteTo.File("logs/log.txt", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
                .WriteTo.MSSqlServer(strConn, sinkOptions: new MSSqlServerSinkOptions
                { TableName = "LogTest", AutoCreateSqlTable = true, BatchPeriod = TimeSpan.FromSeconds(10), BatchPostingLimit = 100 },
                    columnOptions: columnOptions,
                   restrictedToMinimumLevel: LogEventLevel.Debug)
            .CreateLogger();

            //这里用替换原生日志
            builder.Host.UseSerilog();
            return builder;
        }
    }
}
