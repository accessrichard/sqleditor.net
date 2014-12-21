namespace SqlEditor.Models
{
    using System.Collections.Generic;

    using SqlEditor.Models.FileSystem;

    /// <summary>
    /// Adds, deletes and reads files and folder contents.
    /// </summary>
    public interface IFileSystemModel
    {
        /// <summary>
        /// Gets a list of files and folders under the base path.
        /// </summary>
        /// <returns>A list of file and folders.</returns>
        IEnumerable<FileFolderNode> GetFileSystem();

        /// <summary>
        /// Gets the contents of a file.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>The contents of the file.</returns>
        string GetFileContents(string path);

        /// <summary>
        /// Gets metadata about a file or folder.
        /// </summary>
        /// <param name="path">The path to the file or folder.</param>
        /// <param name="fileName">The file name if a file.</param>
        /// <returns>Meta data about a file or folder.</returns>
        FileFolderNode GetFileFolderNode(string path, string fileName = "");

        /// <summary>
        /// Recursively lists directory contents.
        /// </summary>
        /// <param name="path">The directory path.</param>
        /// <returns>A list of files and folders in the directory.</returns>
        IEnumerable<FileFolderNode> GetDirectoryContents(string path);

        /// <summary>
        /// Deletes a file or folder.
        /// </summary>
        /// <param name="path">The path to the file or folder.</param>
        void Delete(string path);

        /// <summary>
        /// Adds a file or folder.
        /// </summary>
        /// <param name="path">The path of the file or folder.</param>
        /// <param name="name">The name of the file or folder.</param>
        /// <param name="content">The contents of the file.</param>
        void Add(string path, string name, string content);

        /// <summary>
        /// Saves a file or folder. If the file does not exist
        /// will create one.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="contents">The contents of the file.</param>
        void Save(string path, string contents = "");
    }
}