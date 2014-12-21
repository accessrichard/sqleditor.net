namespace SqlEditor.Json
{
    using System;
    using System.IO;
    using System.Web.Mvc;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Uses JSON.NET instead of the stock .Net one.
    /// </summary>
    public class JsonNetResult : JsonResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonNetResult"/> class. 
        /// </summary>
        public JsonNetResult()
        {
            this.Settings = new JsonSerializerSettings
                                {
                                    ReferenceLoopHandling = ReferenceLoopHandling.Error,
                                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                                };
        }

        /// <summary>
        /// Gets the serializer settings.
        /// </summary>
        public JsonSerializerSettings Settings { get; private set; }

        /// <summary>
        /// Executes an action result.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet
                && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("JSON GET is not allowed");
            }

            var response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(this.ContentType) ? "application/json" : this.ContentType;

            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }

            if (this.Data == null)
            {
                return;
            }

            var scriptSerializer = JsonSerializer.Create(this.Settings);

            using (var sw = new StringWriter())
            {
                scriptSerializer.Serialize(sw, this.Data);
                response.Write(sw.ToString());
            }
        }
    }
}