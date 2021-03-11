using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace DNBMarket.WebUI.Contollers
{
    public class BaseController : Controller
    {
        protected async Task<string> RenderViewAsync(string viewName, object model, IViewEngine viewEngine = null)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.ActionDescriptor.ActionName;

            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var actionContext = new ActionContext(HttpContext, RouteData, ControllerContext.ActionDescriptor);
                var engine = viewEngine ?? HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                var viewResult = engine.FindView(actionContext, viewName, false);
                if (!viewResult.Success)
                    viewResult = engine.GetView(viewName, viewName, false);
                if (!viewResult.Success)
                    throw new Exception($"The view '{viewName}' couldn't find");

                var viewContext = new ViewContext(actionContext, viewResult.View, ViewData, TempData, sw, new HtmlHelperOptions());

                await viewResult.View.RenderAsync(viewContext);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
