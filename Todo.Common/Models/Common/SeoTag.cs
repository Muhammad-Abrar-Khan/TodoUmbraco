using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Todo.Common.Models.Common
{
	public class SeoTag
	{
		public SeoTag() { }
		public SeoTag(IPublishedContent content)
		{
			if (content != null)
			{
				#region Meta Tags
				this.MetaTitle = content.Value("metaTitle")?.ToString() ?? string.Empty;
				this.MetaDescription = content.Value("metaDescription")?.ToString() ?? string.Empty;

				#endregion
			}
		}

		#region Meta Tags
		public string MetaTitle { get; set; }
		public string MetaDescription { get; set; }
		#endregion
	}
}
