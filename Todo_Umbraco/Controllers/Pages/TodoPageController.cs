using Todo.Common.Helpers;
using Todo.Common.Models.CMS;
using Todo.Common.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Serilog;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace Todo_Umbraco.Controllers.Pages
{
    public class TodoPageController : BaseController
    {
        public TodoPageController(ILogger<TodoPageController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor, IVariationContextAccessor variationContextAccessor) : base(logger, compositeViewEngine, umbracoContextAccessor, variationContextAccessor)
        {
        }

        public override IActionResult Index()
        {
            using (new FunctionTracer())
            {
                try
                {
                    return View("~/Views/Pages/TodoPage.cshtml", CurrentPage as TodoPage);
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, ex.Message);
                }
                return View("~/Views/Pages/TodoPage.cshtml");
            }
        }
    }
}
