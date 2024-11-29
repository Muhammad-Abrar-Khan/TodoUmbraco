using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Todo.Core.Repositories;
using System;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Web.Common;

namespace Todo.Core.Composers
{
    public class TodoComposer : IComposer
    {
        // Implementing Compose method from IComposer interface
        public void Compose(IUmbracoBuilder builder)
        {
            try
            {
                // Get the current assembly
                Assembly assembly = Assembly.GetExecutingAssembly();

                // Get all types in the assembly
                var classesInNamespace = assembly.GetTypes();

                foreach (var RepositoryClass in classesInNamespace)
                {
                    string _namespace = RepositoryClass.DeclaringType == null ? RepositoryClass.FullName : RepositoryClass.DeclaringType.FullName;
                    //exclude base/common classes and constructor
                    if (!RepositoryClass.IsAbstract && RepositoryClass.IsClass && _namespace.Contains(".Repositories.") && !_namespace.Contains("Common") && !RepositoryClass.Name.Contains("<>"))
                    {
                        if (_namespace.Contains("API", StringComparison.OrdinalIgnoreCase))
                        {
                            builder.Services.AddScoped(RepositoryClass, serviceProvider =>
                            {
                                var umbracoContextAccessor = serviceProvider.GetRequiredService<IUmbracoContextAccessor>();
                                var contentService = serviceProvider.GetRequiredService<IContentService>();
                                var umbracoHelper = serviceProvider.GetRequiredService<UmbracoHelper>();
                                var variationContextAccessor = serviceProvider.GetRequiredService<IVariationContextAccessor>();

                                return Activator.CreateInstance(RepositoryClass, umbracoContextAccessor, contentService, umbracoHelper, variationContextAccessor);
                            });
                        }
                        else
                        {
                            builder.Services.AddScoped(RepositoryClass);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                throw new ApplicationException("Error during composer execution", ex);
            }
        }
    }
}
