namespace SqlEditor.Models.Tree
{
    /// <summary>
    /// A tree node.
    /// </summary>
    /// <typeparam name="T">The type of node.</typeparam>
    public class Node<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Node{T}"/> class.
        /// </summary>
        /// <param name="data">The tree node data.</param>
        public Node(T data)
        {
            this.Data = data;
            this.Children = new NodeList<T>(this);
        }

        /// <summary>
        /// Gets or sets the tree node children.
        /// </summary>
        public NodeList<T> Children { get; set; }

        /// <summary>
        /// Gets or sets the tree node data.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Gets or sets the parent of the tree node.
        /// </summary>
        public Node<T> Parent { get; set; }
    }
}