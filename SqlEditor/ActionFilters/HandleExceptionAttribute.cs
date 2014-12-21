namespace SqlEditor.ActionFilters
{
    using System.Net;
    using System.Web.Mvc;

    using SqlEditor.Models.Exceptions;

    /// <summary>
    /// Action filter to handle exceptions. 
    /// </summary>
    public class HandleExceptionAttribute : HandleErrorAttribute
    {
        /// <summary>
        /// Handle JSON errors via a 400 status code and the exception message.
        /// </summary>
        /// <param name="filterContext">The exception context.</param>
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest() && filterContext.Exception != null)
            {               
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                object data;
                if (filterContext.HttpContext.Request.IsLocal)
                {
                    data =
                        new
                            {
                                message = filterContext.Exception.Message,
                                stacktrace = filterContext.Exception.StackTrace
                            };
                }
                else if (filterContext.Exception is SqlEditorException)
                {
                    data = new { message = filterContext.Exception.Message };
                }
                else
                {
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    data = new { message = "An unhandled server error occured." };
                }

                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = data
                };

                filterContext.ExceptionHandled = true;
                return;
            }

            base.OnException(filterContext);
        }
    }
}