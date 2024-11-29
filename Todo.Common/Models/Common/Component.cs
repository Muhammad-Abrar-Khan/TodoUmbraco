using Umbraco.Cms.Core.Models.PublishedContent;

namespace Todo.Common.Models.Common
{
    public class Component
    {
        public string CmsName { get; set; }

        public IPublishedContent CurrentCMSPage { get; set; }

        public Component()
        {
            CmsName = "";
            CurrentCMSPage = null;
        }
    }
}
