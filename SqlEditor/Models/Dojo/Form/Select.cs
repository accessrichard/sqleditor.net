namespace SqlEditor.Models.Dojo.Form
{
    /// <summary>
    /// A dojo form select model.
    /// </summary>
    public class Select
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Select"/> class.
        /// </summary>
        /// <param name="label">
        /// The form select label.
        /// </param>
        /// <param name="value">
        /// The form select value.
        /// </param>
        public Select(string label, string value)
        {
            this.Label = label;
            this.Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Select"/> class.
        /// </summary>
        public Select()
        {
        }

        /// <summary>
        /// Gets or sets the select label.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the select value.
        /// </summary>
        public string Value { get; set; }
    }
}