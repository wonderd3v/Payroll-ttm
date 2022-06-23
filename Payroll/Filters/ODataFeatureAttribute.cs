using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.OData.Extensions;

namespace Payroll.Filters
{
    public class ODataFeatureAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var odataFeature = context.HttpContext.ODataFeature();
            if (odataFeature != null && context.Result is OkObjectResult objectResult)
            {
                if (odataFeature.TotalCount != null)
                {
                    objectResult.Value = new { Count = odataFeature.TotalCount, objectResult.Value };
                    context.Result = objectResult;
                    context.HttpContext.Response.Headers.Add("$odatacount", odataFeature.TotalCount.ToString());
                }
            }
            base.OnActionExecuted(context);
        }
    }
}