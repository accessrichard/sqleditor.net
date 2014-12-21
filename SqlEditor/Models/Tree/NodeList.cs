namespace SqlEditor.Models.Tree
{
    using System.Collections.Generic;

    /// <summary>
    /// A strongly typed list of objects representing children of a tree node.
    /// </summary>
    /// <typeparam name="T">The type of node in the node list.</typeparam>
    public class NodeList<T> : List<Node<T>>
    {
        /// <summary>
        /// The parent directory.
        /// </summary>
        private readonly Node<T> parent;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="NodeList{T}"/> class.
        /// </summary>
        /// <param name="parent">The parent node.</param>
        public NodeList(Node<T> parent)
        {
            this.parent = parent;
        }       

        /// <summary>
        /// Adds a child to a tree node.
        /// </summary>
        /// <param name="value">The value of the node to add.</param>
        /// <returns>The added node.</returns>
        public Node<T> Add(T value)
        {
            var node = new Node<T>(value) { Parent = this.parent };
            this.Add(node);
            return node;
        }

        /// <summary>
        /// Adds a range of child nodes.
        /// </summary>
        /// <param name="range">The enumerable to add.</param>
        public void AddRange(IEnumerable<T> range)
        {
            foreach (var item in range)
            {
                this.Add(item);
            }
        }
    }
}
