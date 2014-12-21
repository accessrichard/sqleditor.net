namespace SqlEditor.Models
{
    using System.Collections.Generic;

    using SqlEditor.Models.Dojo.Form;

    /// <summary>
    /// Model for managing database systems.
    /// </summary>
    public interface ISystemsModel
    {
        /// <summary>
        /// Gets a list of systems for use in a combo box.
        /// </summary>
        /// <returns>The combo box values.</returns>
        IEnumerable<Select> GetSystems();
    }
}