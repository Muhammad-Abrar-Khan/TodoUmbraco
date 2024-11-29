using Todo.Common.Helpers;
using Todo.Common.Models.CMS;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Todo_Umbraco.ViewComponents.Components
{
    public class toDoItem : ViewComponent
    { 
        public async Task<IViewComponentResult> InvokeAsync(IPublishedContent component)
        {
            using (new FunctionTracer())
            {
                return View("~/Views/Partials/Components/_ToDoItem.cshtml", component as ToDoItem);
            }
        }
    }
}
