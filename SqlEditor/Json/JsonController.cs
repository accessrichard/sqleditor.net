namespace SqlEditor.Json
{
    using System.Text;
    using System.Web.Mvc;

    /// <summary>
    /// A base controller for Json results.
    /// Uses JSON.NET for serialization to JSON.
    /// </summary>
    public abstract class JsonController : Controller
    {
        /// <summary>
        /// Overrides the stock Microsoft Java script serializer with
        /// JSON.NET.
        /// This provides useful features such as using Java script lowercase 
        /// properties.
        /// </summary>
        /// <param name="data">The object to serialize.</param>
        /// <param name="contentType">The content type.</param>
        /// <param name="contentEncoding">The content encoding.</param>
        /// <param name="behavior">The request behavior.</param>
        /// <returns>The JSON result.</returns>
        protected override JsonResult Json(
            object data,
            string contentType,
            Encoding contentEncoding,
            JsonRequestBehavior behavior)
        {
            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }
    }
}
