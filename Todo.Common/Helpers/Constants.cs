using Microsoft.AspNetCore.Http;


namespace Todo.Common.Helpers
{
    public class Constants
    {
        public const string cms_component_name = "cms_component_name";
        public const string cms_page_name = "cms_page_name";

        #region Languages
        public static class Languages
        {
            public const string Default = "en";
            public static string Current = "";
        }
        #endregion

        #region Root Alias
        public static class RootAlias
        {
            public const string DataSources = "dataSources";
            public const string Site = "site";
        }
        #endregion



    }

}
