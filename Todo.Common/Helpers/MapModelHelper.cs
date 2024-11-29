//using Todo.Common.Models.Common;
//using Umbraco.Cms.Core.Models;
//using Umbraco.Cms.Core.Models.PublishedContent;
//using System.Collections.Generic;
//using Todo.Common.Models;

//namespace Todo.Common.Helpers
//{
//	public class MapModelHelper
//	{
//		public static List<Navigation> GetNavigations(IPublishedContent content)
//		{
//			using (new FunctionTracer())
//			{
//				if (content != null && content.Children.Any())
//				{
//					var navs = new List<Navigation>();
//					foreach (IPublishedContent navGroup in content.Children)
//					{
//						var nav = new Navigation();
//						if (navGroup != null && !navGroup.Children.Any())
//						{
//							var navigationItem = new NavigationItem();
//							navigationItem.Link = new CTA(navGroup.Value<Link>("link"));
//							navigationItem.CSSClass = navGroup.Value<string>("cssClass") ?? string.Empty;
//							nav.NavigationItem = navigationItem;
//						}
//						else if (navGroup != null && navGroup.Children.Any())
//						{
//							var navigationItem = new NavigationItem();
//							var navigationItems = new List<NavigationItem>();
//							navigationItem.Link = new CTA(navGroup.Value<Link>("link"));
//							navigationItem.CSSClass = navGroup.Value<string>("cssClass") ?? string.Empty;
//							nav.NavigationItem = navigationItem;

//							foreach (var navGroupItem in navGroup.Children)
//							{
//								navigationItems.Add(new NavigationItem()
//								{
//									CSSClass = navGroupItem.Value<string>("cssClass") ?? string.Empty,
//									Link = new CTA(navGroupItem.Value<Link>("link"))
//								});
//							}

//							nav.NavigationItems = navigationItems;
//						}

//						navs.Add(nav);
//					}

//					return navs;
//				}

//				return null;
//			}
//		}

//	}
//}
