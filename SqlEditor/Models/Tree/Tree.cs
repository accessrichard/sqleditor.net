namespace SqlEditor.Models.Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A generic tree data structure.
    /// </summary>
    /// <typeparam name="T">The type of the tree nodes.</typeparam>
    public class Tree<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Tree{T}"/> class. 
        /// </summary>
        /// <param name="root">The tree root node.</param>
        public Tree(T root)
        {
            this.Root = new Node<T>(root);
        }

        /// <summary>
        /// Gets or sets the tree root node.
        /// </summary>
        public Node<T> Root { get; set; }

        /// <summary>
        /// Walks the tree recursively and applies a callback.
        /// </summary>
        /// <param name="action">A void method with parameters (node, depth)
        /// Example: 
        /// tree.Walk((node, depth) => Debug.WriteLine("{0}{1}", " ".PadLeft(depth), node.Data.SomeProperty))
        /// </param>        
        public void Walk(Action<Node<T>, int> action)
        {
            action(this.Root, 0);
            this.WalkTree(this.Root.Children, action);
        }

        /// <summary>
        /// Recursively walks the tree.
        /// </summary>
        /// <param name="children">The tree children</param>
        /// <param name="action">A void method with parameters (node, depth)</param>
        /// <param name="depth">The depth of the currently walked node.</param>
        private void WalkTree(IEnumerable<Node<T>> children, Action<Node<T>, int> action, int depth = 0)
        {
            depth++;
            foreach (var child in children)
            {
                action(child, depth);
                if (child.Children.Any())
                {
                    this.WalkTree(child.Children, action, depth);
                }
            }
        }
    }
}
