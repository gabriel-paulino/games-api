using Games.Application.ViewModel.Generic.Output;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Games.Api.Filters
{
    public class CustomValidateModelState : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                return;

            context.Result = new BadRequestObjectResult(
                new RequiredFieldsOutput
                (
                    context.ModelState
                    .SelectMany(f => f.Value.Errors)
                    .Select(s => s.ErrorMessage)
                ));
        }
    }
}