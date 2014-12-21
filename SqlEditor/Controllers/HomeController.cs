namespace SqlEditor.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// The index controller.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// The index action.
        /// </summary>
        /// <returns>The index action result.</returns>
        public ActionResult Index()
        {
            return this.View();
        }
    }
}