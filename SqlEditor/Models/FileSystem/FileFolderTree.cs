namespace SqlEditor.Models.FileSystem
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using SqlEditor.Models.Encoding;
    using SqlEditor.Models.Tree;

    /// <summary>
    /// A tree structure representing the file system.
    /// </summary>
    public class FileFolderTree
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileFolderTree"/> class. 
        /// </summary>
        /// <param name="baseDirectory">The base directory. </param>
        /// <param name="validExtensions">The file filter to apply. </param>
        public FileFolderTree(string baseDirectory, IEnumerable<string> validExtensions)
        {
            this.BaseDirectory = baseDirectory;
            this.ValidExtensions = validExtensions;
            var rootNode = new FileFolderNode
                               {
                                   Id = baseDirectory,
                                   Name = Path.GetDirectoryName(baseDirectory),
                                   ParentId = Directory.GetParent(baseDirectory).FullName,
                                   IsDir = true
                               };

            this.Tree = new Tree<FileFolderNode>(rootNode);
            this.RecursiveDirectorySearch();
        }

        /// <summary>
        /// Gets the base directory.
        /// </summary>
        public string BaseDirectory { get; private set; }

        /// <summary>
        /// Gets the file filter to apply.
        /// </summary>
        public IEnumerable<string> ValidExtensions { get; private set; }

        /// <summary>
        /// Gets the tree data structure of the file system.
        /// </summary>
        public Tree<FileFolderNode> Tree { get; private set; }

        /// <summary>
        /// Converts a tree to a flat list.
        /// Useful because without an external JSON library, the .NET 
        /// serializer cannot handle the tree data structure. It chokes on 
        /// having arrays of arrays (node.children[]).
        /// </summary>
        /// <returns>A files and folders in a flat list.</returns>
        public IEnumerable<FileFolderNode> ToList()
        {
            var list = new List<FileFolderNode>();
            this.Tree.Walk((node, depth) => list.Add(node.Data));
            return list;
        }

        /// <summary>
        /// Converts a tree to a flat list where the file paths are base64 encoded.
        /// </summary>
        /// <returns>A files and folders in a flat list.</returns>
        public IEnumerable<FileFolderNode> ToEncodedList()
        {
            var list = new List<FileFolderNode>();
            this.Tree.Walk((node, depth) => list.Add(this.EncodeFilePaths(node.Data)));
            return list;
        }

        /// <summary>
        /// Encodes the file paths in a file folder node to base 64.
        /// </summary>
        /// <param name="node">
        /// The file folder node.
        /// </param>
        /// <returns>
        /// The <see cref="FileFolderNode"/>.
        /// </returns>
        private FileFolderNode EncodeFilePaths(FileFolderNode node)
        {
            node.Id = Base64.Encode(node.Id);
            node.ParentId = Base64.Encode(node.ParentId);
            return node;
        }

        /// <summary>
        /// Searches a directory for files and folders.
        /// </summary>
        private void RecursiveDirectorySearch()
        {
            this.PopulateFilesInFolder(this.BaseDirectory, this.Tree.Root);
            this.RecurseDirectory(this.BaseDirectory, this.Tree.Root);
        }

        /// <summary>
        /// Searches for files and folders in a directory.
        /// </summary>
        /// <param name="directory">
        /// The directory path.
        /// </param>
        /// <param name="node">
        /// The current node.
        /// </param>
        private void PopulateFilesInFolder(string directory, Node<FileFolderNode> node)
        {
            node.Children.AddRange(
                from file in Directory.GetFiles(directory)
                where this.ValidExtensions.Any(file.EndsWith)
                select
                    new FileFolderNode { Name = Path.GetFileName(file), Id = file, ParentId = directory, IsDir = false });
        }

        /// <summary>
        /// Searches a directory for files and folders.
        /// </summary>
        /// <param name="dir">
        /// The directory.
        /// </param>
        /// <param name="parentNode">
        /// The parent node.
        /// </param>
        private void RecurseDirectory(string dir, Node<FileFolderNode> parentNode)
        {
            foreach (var directory in Directory.GetDirectories(dir))
            {
                var fileFolderNode = new FileFolderNode
                {
                    Name = Path.GetFileName(directory),
                    Id = directory,
                    ParentId = dir,
                    IsDir = true
                };
                var newNode = new Node<FileFolderNode>(fileFolderNode);
                this.PopulateFilesInFolder(directory, newNode);
                parentNode.Children.Add(newNode);
                this.RecurseDirectory(directory, newNode);
            }
        }
    }
}
