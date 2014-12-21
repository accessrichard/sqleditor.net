namespace SqlEditor.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using SqlEditor.Models.Config;
    using SqlEditor.Models.Encoding;
    using SqlEditor.Models.Exceptions;
    using SqlEditor.Models.FileSystem;

    using StructureMap;

    /// <inheritdoc />
    public class FileSystemModel : IFileSystemModel
    {
        /// <summary>
        /// The base path of the files and folders that are allowed to be read
        /// or modified.
        /// </summary>
        private readonly string baseDirectory;

        /// <summary>
        /// A list of valid extensions.
        /// </summary>
        private readonly IEnumerable<string> allowableExtensions;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemModel"/> class. 
        /// </summary>
        /// <param name="baseDirectory">The base path of files and folders allowed to be modified. </param>
        /// <param name="allowableExtensions">A list of valid extensions. </param>
        public FileSystemModel(string baseDirectory, IEnumerable<string> allowableExtensions)
        {
            this.baseDirectory = baseDirectory;
            this.allowableExtensions = allowableExtensions;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemModel"/> class. 
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        [DefaultConstructor]
        public FileSystemModel(IConfig config)
        {
            this.baseDirectory = config.BaseDirectory;
            this.allowableExtensions = config.AllowableExtensions;
        }

        /// <inheritdoc />
        public IEnumerable<FileFolderNode> GetFileSystem()
        {
            return new FileFolderTree(this.baseDirectory, this.allowableExtensions).ToEncodedList();
        }

        /// <inheritdoc />
        public string GetFileContents(string path)
        {
            this.ValidatePathUnderAllowableBase(path);
            this.ValidateExtension(path);
            return File.Exists(path) ? File.ReadAllText(path) : string.Empty;
        }

        /// <inheritdoc />
        public FileFolderNode GetFileFolderNode(string path, string fileName = "")
        {
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                path = Path.Combine(path, fileName);
            }

            var isDir = (File.GetAttributes(path) & FileAttributes.Directory)
                 == FileAttributes.Directory;

            return new FileFolderNode
                       {
                           Id = Base64.Encode(path),
                           ParentId = Base64.Encode(Directory.GetParent(path).FullName),
                           Name = isDir ? Path.GetDirectoryName(path) : Path.GetFileName(path),
                           IsDir = isDir
                       };
        }

        /// <inheritdoc />
        public IEnumerable<FileFolderNode> GetDirectoryContents(string path)
        {
            this.ValidatePathUnderAllowableBase(path);
            return !Directory.Exists(path) ?
                new List<FileFolderNode>() :
                new FileFolderTree(path, this.allowableExtensions).ToEncodedList();
        }

        /// <inheritdoc />
        public void Delete(string path)
        {
            this.ValidatePathUnderAllowableBase(path);
            if (Directory.Exists(path))
            {
                Directory.Delete(path);
                return;
            }

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        /// <inheritdoc />
        public void Add(string path, string name = "", string content = "")
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                path = Path.Combine(path, name);
            }

            this.ValidatePathUnderAllowableBase(path);
            if (string.IsNullOrWhiteSpace(Path.GetExtension(path)))
            {
                Directory.CreateDirectory(path);
                return;
            }

            this.ValidateExtension(path);
            File.WriteAllText(path, content);
        }

        /// <inheritdoc />
        public void Save(string path, string contents = "")
        {
            this.ValidateExtension(path);
            this.ValidatePathUnderAllowableBase(path);

            if (!File.Exists(path))
            {
                this.Add(path);
            }

            File.WriteAllText(path, contents);
        }

        /// <summary>
        /// Validates a path is under an allowable base path
        /// for security.
        /// </summary>
        /// <param name="path">The path.</param>
        private void ValidatePathUnderAllowableBase(string path)
        {
            if (this.IsPathUnderAllowableBase(path))
            {
                return;
            }

            throw new SqlEditorException("Attempting to modify file outside the base path of: " + this.baseDirectory);
        }

        /// <summary>
        /// Validates the file extension is in an allowable
        /// extension.
        /// </summary>
        /// <param name="path">The path.</param>
        private void ValidateExtension(string path)
        {
            if (this.IsValidExtension(path))
            {
                return;
            }

            throw new SqlEditorException(string.Format(
                    "Invalid Extension. Extension is: {0}, only extensions of {1} are allowed.",
                    Path.GetExtension(path),
                    string.Join(", ", this.allowableExtensions).Replace("*", string.Empty)));
        }

        /// <summary>
        /// Validates a path is under a base path.
        /// </summary>
        /// <param name="path">The path. </param>
        /// <returns>
        /// A value indicating whether the path is under an allowable base path.
        /// </returns>
        private bool IsPathUnderAllowableBase(string path)
        {
            return this.IsSubDirectory(this.baseDirectory, path);
        }

        /// <summary>
        /// Determines whether a path is a subdirectory of a base path.
        /// </summary>
        /// <param name="pathBase">The base path.</param>
        /// <param name="path">The path to check.</param>
        /// <returns>A value indicating whether the base path is under the path.</returns>
        private bool IsSubDirectory(string pathBase, string path)
        {
            var baseDirInfo = new DirectoryInfo(this.RemoveTrailingSlash(pathBase));
            var pathDirInfo = new DirectoryInfo(this.RemoveTrailingSlash(path));
            if (baseDirInfo.FullName == pathDirInfo.FullName)
            {
                return true;
            }

            while (pathDirInfo.Parent != null)
            {
                if (pathDirInfo.Parent.FullName == baseDirInfo.FullName)
                {
                    return true;
                }

                pathDirInfo = pathDirInfo.Parent;
            }

            return false;
        }

        /// <summary>
        /// Removes the trailing slash from a file. 
        ///    e.g. c:\Users\ -> c:\Users
        /// </summary>
        /// <param name="path">The path to validate.</param>
        /// <returns>The normalized path.</returns>
        private string RemoveTrailingSlash(string path)
        {
            var trailingSlash = path.Substring(path.Length - 1, 1);
            return trailingSlash == "/" || trailingSlash == "\\" ? path.Substring(0, path.Length - 1) : path;
        }

        /// <summary>
        /// Validates whether the file extension is valid.
        /// </summary>
        /// <param name="file">The file to check.</param>
        /// <returns>A value indicating whether the extension is valid.</returns>
        private bool IsValidExtension(string file)
        {
            var extension = Path.GetExtension(file);
            return !string.IsNullOrWhiteSpace(extension) &&
                this.allowableExtensions.Any(e => string.Equals(e, extension, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}