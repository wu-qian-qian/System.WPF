using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Model;

namespace System.Api.Filters
{
    public class ResultFilters : IAsyncResultFilter
    {


        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            //进行类型的区分返回
            ObjectResult result = context.Result as ObjectResult;
            //直接返回
            HttpResult httpResult = new HttpResult();
            httpResult.Code = 200;
            httpResult.IsSuccess = true;
            httpResult.Result = result.Value;
            context.Result = new ObjectResult(httpResult);
            await next();
        }
    }
}
