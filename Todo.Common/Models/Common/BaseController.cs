using Todo.Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace Todo.Common.Models.Common
{
    public class BaseController : RenderController
    {
        private readonly ILogger _logger;
        private readonly IVariationContextAccessor _variationContextAccessor;

        public BaseController(ILogger<BaseController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor, IVariationContextAccessor variationContextAccessor) : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _logger = logger;
            _variationContextAccessor = variationContextAccessor;
            Constants.Languages.Current = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            _variationContextAccessor.VariationContext = new VariationContext(Constants.Languages.Current);
        }
    }
}
