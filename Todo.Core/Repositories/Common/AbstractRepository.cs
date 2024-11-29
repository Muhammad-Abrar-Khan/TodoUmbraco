using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common;

namespace Todo.Core.Repositories.Common
{
    public abstract class AbstractRepository
    {
        protected readonly IUmbracoContextAccessor _umbracoContextAccessor;
        protected readonly IContentService _contentService;
        protected readonly UmbracoHelper _umbracoHelper;
        protected readonly IVariationContextAccessor _variationContextAccessor;



        protected AbstractRepository(IUmbracoContextAccessor umbracoContextAccessor, IContentService contentService, UmbracoHelper umbracoHelper, IVariationContextAccessor variationContextAccessor)
        {
            _umbracoContextAccessor = umbracoContextAccessor;
            _contentService = contentService;
            _umbracoHelper = umbracoHelper;
            _variationContextAccessor = variationContextAccessor;
        }
    }
}
