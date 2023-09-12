using Wata.Commerce.Common.Helpers;

namespace Wata.Commerce.Account.Module.Models.User
{
    public interface IUserFilters
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
        /// The <see cref="UserFilterColumns"/> being filtered on.
        /// </summary>
        UserFilterColumns FilterColumn { get; set; }

        /// <summary>
        /// The <see cref="UserFilterColumns"/> being sorted.
        /// </summary>
        UserFilterColumns SortColumn { get; set; }

        string? FilterText { get; set; }
    }
}