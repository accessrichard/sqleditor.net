namespace SqlEditor.Models.Dojo.Dgrid
{
    /// <summary>
    /// Represents a column in the client side dojo grid. 
    /// </summary>
    public class Column
    {
        /// <summary>
        /// Backing field for label property.
        /// </summary>
        private string label;

        /// <summary>
        /// Gets or sets the dojo grid field name.
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Gets or sets a JS formatter name to use in order to 
        /// format the column in the grid using JS.
        /// </summary>
        public string Formatter { get; set; }

        /// <summary>
        /// Gets a value indicating whether the field is sortable in the client.
        /// </summary>
        public bool Sortable
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets or sets the data grid column header label.
        /// </summary>
        public string Label
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.label) ? this.Field : this.label;
            }

            set
            {
                this.label = value;
            }
        }
    }
}
