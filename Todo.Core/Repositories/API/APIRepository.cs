using Todo.Common.Helpers;
//using Todo.Common.Models.API.Request;
using Todo.Common.Models.API.Response;
using Todo.Common.Models.CMS;
using Todo.Core.Repositories.Common;
using Serilog;
using System.Security.Cryptography.Xml;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common;
using CommonHelpers = Todo.Common.Helpers.Common;

namespace Todo.Core.Repositories.API
{
    public class APIRepository : AbstractRepository
    {

        public APIRepository(IUmbracoContextAccessor umbracoContextAccessor, IContentService contentService, UmbracoHelper umbracoHelper, IVariationContextAccessor variationContextAccessor) : base(umbracoContextAccessor, contentService, umbracoHelper, variationContextAccessor)
        {
        }

        public async Task<ApiResponse> CreateToDoItem(string title, string description, bool isCompleted)
        {
            try
            {
                var newItem = _contentService.Create("ToDoItem", -1, "ToDoItem");
                newItem.SetValue("title", title);
                newItem.SetValue("description", description);
                newItem.SetValue("isCompleted", isCompleted);

                _contentService.SaveAndPublish(newItem);

                return ApiResponse.Okay(newItem, "ToDo item created successfully.");
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Error creating ToDo item");
                return ApiResponse.BadRequest("Error creating ToDo item.");
            }
        }

        public ApiResponse GetAllToDoItems()
        {
            try
            {
                var todoItems = _contentService.GetContentOfContentType("ToDoItem")
                                               .Select(x => new
                                               {
                                                   Title = x.GetValue<string>("title"),
                                                   Description = x.GetValue<string>("description"),
                                                   IsCompleted = x.GetValue<bool>("isCompleted")
                                               })
                                               .ToList();

                return ApiResponse.Okay(todoItems, "Fetched all ToDo items.");
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Error fetching ToDo items");
                return ApiResponse.NotFound("ToDo items not found.");
            }
        }

        public ApiResponse GetToDoItemById(int id)
        {
            try
            {
                var todoItem = _contentService.GetById(id);
                if (todoItem == null)
                    return ApiResponse.NotFound("ToDo item not found.");

                var result = new
                {
                    Title = todoItem.GetValue<string>("title"),
                    Description = todoItem.GetValue<string>("description"),
                    IsCompleted = todoItem.GetValue<bool>("isCompleted")
                };

                return ApiResponse.Okay(result, "ToDo item found.");
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Error fetching ToDo item by ID");
                return ApiResponse.NotFound("ToDo item not found.");
            }
        }

        public async Task<ApiResponse> UpdateToDoItem(int id, string title, string description, bool isCompleted)
        {
            try
            {
                var todoItem = _contentService.GetById(id);
                if (todoItem == null)
                    return ApiResponse.NotFound("ToDo item not found.");

                todoItem.SetValue("title", title);
                todoItem.SetValue("description", description);
                todoItem.SetValue("isCompleted", isCompleted);

                _contentService.SaveAndPublish(todoItem);

                return ApiResponse.Okay(todoItem, "ToDo item updated successfully.");
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Error updating ToDo item");
                return ApiResponse.BadRequest("Error updating ToDo item.");
            }
        }

        public ApiResponse DeleteToDoItem(int id)
        {
            try
            {
                var todoItem = _contentService.GetById(id);
                if (todoItem == null)
                    return ApiResponse.NotFound("ToDo item not found.");

                _contentService.Delete(todoItem);

                return ApiResponse.Okay(null, "ToDo item deleted successfully.");
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Error deleting ToDo item");
                return ApiResponse.BadRequest("Error deleting ToDo item.");
            }
        }


        public string TestApi()
        {
            using (new FunctionTracer())
            {
                string RetVal = "";
                try
                {
                    RetVal = "Hello World";

                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, ex.Message);
                }
                return RetVal;
            }
        }

    }
}
