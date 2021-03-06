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
        /// Backing field for field property.
        /// </summary>
        private string field;
       
        /// <summary>
        /// Gets or sets the dojo grid field name.
        /// </summary>
        public string Field 
        {
            get
            {
                return this.field;
            }

            set
            {
                this.field = value.ToLower();
                this.OriginalField = value;
            }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether the field is displayed in the grid.
        /// </summary>
        public bool Hidden { get; set; }

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
                return string.IsNullOrWhiteSpace(this.label) ? this.OriginalField : this.label;
            }

            set
            {
                this.label = value;
            }
        }

        /// <summary>
        /// Gets or sets the original field value unmodified.
        /// </summary>
        private string OriginalField { get; set; }
    }
}
