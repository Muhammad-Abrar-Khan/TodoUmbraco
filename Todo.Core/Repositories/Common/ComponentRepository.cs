using Todo.Common.Helpers;
using Todo.Common.Models.Common;
using Serilog;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Todo.Core.Repositories.Common
{
    public abstract class ComponentRepository
    {
        public virtual Component GetContent(IPublishedContent content)
        {
            using (new FunctionTracer())
            {
                if (content == null)
                {
                    return null;
                }
                Component model = new Component();
                try
                {
                    model.CmsName = content.ContentType.Alias;
                    model.CurrentCMSPage = content;
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, ex.Message);
                }
                return model;
            }
        }
    }
}
