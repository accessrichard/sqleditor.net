namespace SqlEditor.Controllers
{
    using System.Web.Mvc;

    using SqlEditor.Json;
    using SqlEditor.Models;

    /// <summary>
    /// The database systems controller.
    /// </summary>
    public class SystemsController : JsonController
    {
        /// <summary>
        /// The model.
        /// </summary>
        private readonly ISystemsModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemsController"/> class. 
        /// </summary>
        /// <param name="model">The model.</param>
        public SystemsController(ISystemsModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Gets a list of database systems.
        /// </summary>
        /// <returns>A list of database systems.</returns>
        [HttpGet]
        public JsonResult Index()
        { 
            return this.Json(this.model.GetSystems(), JsonRequestBehavior.AllowGet);
        }
    }
}