using Wata.Commerce.Common.Helpers;

namespace Wata.Commerce.Sample.Module.Models.Abc
{
    public interface IAbcFilters
    {
        /// <summary>
        /// Loading indicator.
        /// </summary>
        bool Loading { get; set; }

        /// <summary>
        /// Paging state in <see cref="PageHelper"/>.
        /// </summary>
        IPageHelper PageHelper { get; set; }

        /// <summary>
        /// Gets or sets a value indicating if the sort is ascending or descending.
        /// </summary>
        bool SortDescending { get; set; }

        /// <summary>
        /// The <see cref="AbcFilterColumns"/> being filtered on.
        /// </summary>
        AbcFilterColumns FilterColumn { get; set; }

        /// <summary>
        /// The <see cref="AbcFilterColumns"/> being sorted.
        /// </summary>
        AbcFilterColumns SortColumn { get; set; }

        string? FilterText { get; set; }
    }
}