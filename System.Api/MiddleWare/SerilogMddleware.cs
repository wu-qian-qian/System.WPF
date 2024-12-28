using Model.Logs;
using Serilog;
using System.Api.Options;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace System.Api.MiddleWare
{
    public class SerilogMddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<SerilogMddleware> _logger;
        private readonly LogOptions _logOptions;
        private  Stopwatch _stopwatch;

        public SerilogMddleware(RequestDelegate next, ILogger<SerilogMddleware> logger, LogOptions logOptions)
        {
            this.next = next;
            _logger = logger;
            _logOptions = logOptions;
            _stopwatch=new Stopwatch();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _stopwatch.Restart();
            HttpRequest request = context.Request;
            // 获取Response.Body内容
            var originalBodyStream = context.Response.Body;
            string responseData = string.Empty;
            string requestData= string.Empty;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;
                await next(context);
                responseData =await GetResponse(context.Response);
                await responseBody.CopyToAsync(originalBodyStream);
            }
            //可以让 Request.Body 可以再次读取
            if (request.ContentLength!=null)
            {
                request.EnableBuffering();
                Stream stream = request.Body;
                byte[] buffer = new byte[stream.Length];
                await stream.ReadAsync(buffer, 0, buffer.Length);
                request.Body.Position = 0;
                requestData = Encoding.UTF8.GetString(buffer);
            }
            _stopwatch.Stop();
            if (_logOptions.IsRecordGet == false && request.Method == "GET")
            {
                return;
            }
            if (_logOptions.IsRecord.Contains(request.Path))
            {
                return;
            }
            // 获取请求body内容
            LogModel log = new LogModel()
            {
                IP = context.Connection.RemoteIpAddress.ToString(),
                RequstUri = context.Request.Path,
                RequstParameter = requestData,
                EndTimeData = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                ResponseResult = responseData,
                TimeTaken = _stopwatch.ElapsedMilliseconds,
            };
            Log.Logger.ForContext(nameof(LogModel.IP), log.IP)
                .ForContext(nameof(LogModel.RequstUri), log.RequstUri)
                .ForContext(nameof(LogModel.RequstParameter), log.RequstParameter)
                .ForContext(nameof(LogModel.EndTimeData), log.EndTimeData)
                .ForContext(nameof(LogModel.TimeTaken), log.TimeTaken)
                .ForContext(nameof(LogModel.ResponseResult), log.ResponseResult).Information("");
        }

        /// <summary>
        /// 获取响应内容
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public async ValueTask<string> GetResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return text;
        }
    }
}
