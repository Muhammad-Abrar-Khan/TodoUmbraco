using Todo.Common.Helpers;
using Todo.Common.Models.Common;
using Serilog;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Extensions;
using Umbraco.Extensions;

namespace Todo.Core.Repositories.Common
{
	public abstract class PageRepository
	{
		public virtual Page GetPageData(IPublishedContent content)
		{
			using (new FunctionTracer())
			{
				if (content == null)
				{
					return null;
				}
				Page model = new Page(content);
				try
				{
                    IEnumerable<IPublishedContent> components = (IEnumerable<IPublishedContent>)content.Value<IEnumerable<IPublishedElement>>("componentPicker");

                    if (components != null && components.Any())
					{
						model.ComponentPicker = components;
					}
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
