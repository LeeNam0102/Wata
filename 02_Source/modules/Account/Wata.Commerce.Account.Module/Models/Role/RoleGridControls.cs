using Wata.Commerce.Common.Helpers;

namespace Wata.Commerce.Account.Module.Models.Role
{
    /// <summary>
    /// State of grid filters.
    /// </summary>
    public class RoleGridControls : IRoleFilters
    {
        /// <summary>
        /// Keep state of paging.
        /// </summary>
        public IPageHelper PageHelper { get; set; }

        public RoleGridControls(IPageHelper pageHelper)
        {
            PageHelper = pageHelper;
        }

        /// <summary>
        /// Avoid multiple concurrent requests.
        /// </summary>
        public bool Loading { get; set; }

        /// <summary>
        /// Column to sort by.
        /// </summary>
        public RoleFilterColumns SortColumn { get; set; }
            = RoleFilterColumns.Id;

        /// <summary>
        /// True when sorting ascending, otherwise sort descending.
        /// </summary>
        public bool SortDescending { get; set; }

        /// <summary>
        /// Column filtered text is against.
        /// </summary>
        public RoleFilterColumns FilterColumn { get; set; }
            = RoleFilterColumns.Id;

        /// <summary>
        /// Text to filter on.
        /// </summary>
        public string? FilterText { get; set; }


    }
}