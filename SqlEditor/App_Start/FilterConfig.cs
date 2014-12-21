namespace SqlEditor
{
    using System.Web.Mvc;

    using SqlEditor.ActionFilters;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleExceptionAttribute());
        }
    }
}