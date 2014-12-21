namespace SqlEditor.Models.Dojo.Form
{
    /// <summary>
    /// A Dojo form combo box item.
    /// </summary>
    public class ComboBox
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComboBox"/> class. 
        /// </summary>
        /// <param name="name">The combo box item name.</param>
        /// <param name="id">The combo box item id.</param>
        public ComboBox(string name, string id)
        {
            this.Name = name;
            this.Id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComboBox"/> class. 
        /// </summary>
        public ComboBox()
        {
        }

        /// <summary>
        /// Gets or sets the displayed combo box label.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the id of the combo box name.
        /// </summary>
        public string Id { get; set; }
    }
}