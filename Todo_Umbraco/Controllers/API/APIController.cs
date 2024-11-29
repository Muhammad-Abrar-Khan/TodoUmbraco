using Todo.Common.Helpers;
//using Todo.Common.Models.API.Request;
using Todo.Common.Models.API.Response;
using Todo.Core.Repositories.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Serilog;
using System.Net;
using System.Text.RegularExpressions;
using Todo.Common.Models.CMS;

namespace Todo_Umbraco.Controllers.API
{
    [Route("api")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly APIRepository _repository;
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;

        public APIController(APIRepository repository,
            IRazorViewEngine razorViewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider)
        {
            _repository = repository;
            _razorViewEngine = razorViewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
        }

        [HttpGet("Hello")]
        public string Hello()
        {
            using (new FunctionTracer())
            {
                return _repository.TestApi();
            }
        }


        [HttpPost("CreateToDoItem")]
        public async Task<IActionResult> CreateToDoItem([FromBody] ToDoItem model)
        {
            var response = await _repository.CreateToDoItem(model.Title, model.Description, model.IsCompleted);
            return StatusCode((int)response.statusCode, response);
        }

        [HttpGet("GetAllToDoItems")]
        public IActionResult GetAllToDoItems()
        {
            var response = _repository.GetAllToDoItems();
            return StatusCode((int)response.statusCode, response);
        }

        [HttpGet("GetToDoItem/{id}")]
        public IActionResult GetToDoItemById(int id)
        {
            var response = _repository.GetToDoItemById(id);
            return StatusCode((int)response.statusCode, response);
        }

        [HttpPut("UpdateToDoItem/{id}")]
        public async Task<IActionResult> UpdateToDoItem(int id, [FromBody] ToDoItem model)
        {
            var response = await _repository.UpdateToDoItem(id, model.Title, model.Description, model.IsCompleted);
            return StatusCode((int)response.statusCode, response);
        }

        [HttpDelete("DeleteToDoItem/{id}")]
        public IActionResult DeleteToDoItem(int id)
        {
            var response = _repository.DeleteToDoItem(id);
            return StatusCode((int)response.statusCode, response);
        }


        private async Task<string> RenderViewToStringAsync(string viewName, object model)
        {
            var actionContext = new ActionContext(HttpContext, RouteData, ControllerContext.ActionDescriptor);
            var viewResult = _razorViewEngine.GetView(null, viewName, false);

            if (!viewResult.Success)
            {
                throw new FileNotFoundException($"View '{viewName}' not found.");
            }

            using (var sw = new StringWriter())
            {
                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()) { Model = model },
                    new TempDataDictionary(HttpContext, _tempDataProvider),
                    sw,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                string result = sw.ToString();
                // Remove all newline characters
                result = result.Replace("\r\n", "").Replace("\n", "");

                // Optionally, also remove excess whitespace between elements
                result = Regex.Replace(result, @"\s+", " "); // Collapse any remaining whitespace to a single space
                result = result.Trim(); // Trim leading and trailing whitespace

                return result;
            }
        }
    }
}
