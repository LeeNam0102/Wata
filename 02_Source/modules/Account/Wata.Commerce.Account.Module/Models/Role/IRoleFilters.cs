using Wata.Commerce.Common.Helpers;

namespace Wata.Commerce.Account.Module.Models.Role
{
    public interface IRoleFilters
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
        /// The <see cref="RoleFilterColumns"/> being filtered on.
        /// </summary>
        RoleFilterColumns FilterColumn { get; set; }

        /// <summary>
        /// The <see cref="RoleFilterColumns"/> being sorted.
        /// </summary>
        RoleFilterColumns SortColumn { get; set; }

        string? FilterText { get; set; }
    }
}