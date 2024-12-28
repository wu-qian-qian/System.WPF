using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Model;

namespace System.Api.Filters
{
    public class ExceptionFilters : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            string msg = context.Exception.Message.ToString();
            HttpResult<string> httpResult = new HttpResult<string>();
            httpResult.Code = 500;
            httpResult.IsSuccess = false;
            httpResult.Result = msg;
            ObjectResult result = new ObjectResult(httpResult);
            context.Result = result;
            //表示已执行
            context.ExceptionHandled = true;
        }
    }
}
