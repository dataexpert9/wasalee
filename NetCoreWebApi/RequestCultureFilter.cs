using Component.Culture;
using Component.Utility;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wasalee
{
    public class RequestCultureFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            StringValues language;
            context.HttpContext.Request.Headers.TryGetValue("Accept-Language", out language);
            string cultureShort = language.ToString().Split('-')[0].ToLower();
            switch (cultureShort)
            {
                case "ar":
                    CultureHelper.Culture = CultureType.Arabic;
                    break;
                case "en":
                    CultureHelper.Culture = CultureType.English;
                    break;
                default:
                    CultureHelper.Culture = CultureType.English;
                    break;
            }
            base.OnActionExecuting(context);
        }
    }
}
