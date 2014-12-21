namespace SqlEditor.Controllers
{
    using System.Web.Mvc;

    using SqlEditor.Json;
    using SqlEditor.Models;
    using SqlEditor.Models.Encoding;

    /// <summary>
    /// The IO controller for reading and manipulating the file system.
    /// </summary>
    public class IoController : JsonController
    {
        /// <summary>
        /// The model.
        /// </summary>
        private readonly IFileSystemModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="IoController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        public IoController(IFileSystemModel model)
        {
            this.model = model;
        }
        
        /// <summary>
        /// Gets a list of files and folders under the configuration base directory path.
        /// </summary>
        /// <returns>
        /// The list of files and folders in the base directory path.
        /// </returns>
        [HttpGet]
        [Route("io")]
        public JsonResult ListFileSystemContents()
        {
            return this.Json(this.model.GetFileSystem(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the contents of a file or folder.
        /// </summary>
        /// <param name="id">The base 64 encoded file path.</param>
        /// <returns>The contents of a file.</returns>
        [HttpGet]
        [Route("io/{id}")]
        public JsonResult Get(string id)
        {
            return this.Json(this.model.GetFileContents(Base64.Decode(id)), JsonRequestBehavior.AllowGet);
        }       

        /// <summary>
        /// Adds a file or folder.
        /// </summary>
        /// <param name="parentId"> The base64 encoded parent path of the file or folder. </param>
        /// <param name="name"> The name of the file or folder. </param>
        /// <param name="content"> The content of the file. </param>
        /// <returns> An empty result </returns>
        [HttpPost]
        [Route("io")]
        public JsonResult Add(string parentId, string name, string content)
        {
            var path = Base64.Decode(parentId);
            this.model.Add(path, name, content);
            return this.Json(this.model.GetFileFolderNode(path, name));
        }

        /// <summary>
        /// Saves a file.
        /// </summary>
        /// <param name="id"> The base 64 encoded file path. </param>
        /// <param name="content"> The content of the file. </param>
        /// <returns> The <see cref="JsonResult"/>. </returns>
        [HttpPut]
        [Route("io/{id}")]
        public JsonResult Save(string id, string content)
        {
            var path = Base64.Decode(id);
            this.model.Save(path, content);
            return this.Json(this.model.GetFileFolderNode(path));
        }

        /// <summary>
        /// Deletes a file or folder.
        /// </summary>
        /// <param name="id">
        /// The base64 encoded path to the file or folder.
        /// </param>
        /// <returns> An empty response. </returns>
        [HttpDelete]
        [Route("io/{id}")]
        public JsonResult Delete(string id)
        {
            this.model.Delete(Base64.Decode(id));
            return this.Json(new { });
        }
    }
}
