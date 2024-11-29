
using Microsoft.AspNetCore.Components.Web;
using Umbraco.Cms.Core.Models.PublishedContent;
using System.Collections.Generic;

namespace Todo.Common.Models.Common
{
	public class Page : SeoTag
	{
		public string CmsName { get; set; }
        public string PageTitle { get; set; }
        public IPublishedContent CurrentCMSPage { get; set; }

		public Page(IPublishedContent content) : base(content)
		{
            PageTitle = content.Name;
            CmsName = content.ContentType.Alias;
            CurrentCMSPage = content;
        }
		
		public Page()
		{

		}
		
		public IEnumerable<IPublishedContent> ComponentPicker { get; set; }
	}
}
