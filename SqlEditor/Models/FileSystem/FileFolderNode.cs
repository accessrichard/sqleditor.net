namespace SqlEditor.Models.FileSystem
{
    /// <summary>
    /// A file or folder node.
    /// </summary>
    public class FileFolderNode
    {
        /// <summary>
        /// Gets or sets the base64 encoded path to the
        /// file or folder.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the base64 encoded path to the parent folder.
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// Gets or sets the file or folder name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the node is a folder or not.
        /// </summary>
        public bool IsDir { get; set; }
    }
}